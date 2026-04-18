using FlightTicketManager.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FlightTicketManager.DAL
{
    public class BookingDAL
    {
        private readonly string connString =
            ConfigurationManager.ConnectionStrings["FlightDbConn"].ConnectionString;

        public string CreateBooking(BookingRequestDTO request)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    string bookingCode = "BK" + DateTime.Now.ToString("yyyyMMddHHmmss");

                    string insertBooking = @"
                        INSERT INTO Booking
                        (
                            BookingCode, CustomerProfileID, CreatedByProfileID, PromotionID,
                            BookingChannel, BookingStatus,
                            ContactFullName, ContactEmail, ContactPhone,
                            HoldExpiredAt, OriginalAmount, DiscountAmount, FinalAmount
                        )
                        OUTPUT INSERTED.BookingID
                        VALUES
                        (
                            @BookingCode, @CustomerProfileID, @CreatedByProfileID, NULL,
                            'Online', 'Pending',
                            @ContactFullName, @ContactEmail, @ContactPhone,
                            @HoldExpiredAt, @OriginalAmount, @DiscountAmount, @FinalAmount
                        )";

                    SqlCommand cmdBooking = new SqlCommand(insertBooking, conn, trans);
                    cmdBooking.Parameters.AddWithValue("@BookingCode", bookingCode);
                    cmdBooking.Parameters.AddWithValue("@CustomerProfileID", request.CustomerProfileID);
                    cmdBooking.Parameters.AddWithValue("@CreatedByProfileID", request.CreatedByProfileID);
                    cmdBooking.Parameters.AddWithValue("@ContactFullName", request.ContactFullName);
                    cmdBooking.Parameters.AddWithValue("@ContactEmail", (object)request.ContactEmail ?? DBNull.Value);
                    cmdBooking.Parameters.AddWithValue("@ContactPhone", request.ContactPhone);
                    cmdBooking.Parameters.AddWithValue("@HoldExpiredAt", request.HoldExpiredAt);
                    cmdBooking.Parameters.AddWithValue("@OriginalAmount", request.OriginalAmount);
                    cmdBooking.Parameters.AddWithValue("@DiscountAmount", request.DiscountAmount);
                    cmdBooking.Parameters.AddWithValue("@FinalAmount", request.FinalAmount);

                    int bookingId = (int)cmdBooking.ExecuteScalar();

                    foreach (var p in request.Passengers)
                    {
                        // 1. giữ ghế trước
                        string holdSeat = @"
                            UPDATE Seat
                            SET SeatStatus = 'Held'
                            WHERE SeatID = @SeatID
                              AND FlightID = @FlightID
                              AND SeatStatus = 'Available'";

                        SqlCommand cmdHoldSeat = new SqlCommand(holdSeat, conn, trans);
                        cmdHoldSeat.Parameters.AddWithValue("@SeatID", p.SeatID);
                        cmdHoldSeat.Parameters.AddWithValue("@FlightID", request.FlightID);

                        int rowAffected = cmdHoldSeat.ExecuteNonQuery();
                        if (rowAffected == 0)
                            throw new Exception("Ghế " + p.SeatID + " đã có người chọn hoặc không còn trống.");

                        // 2. thêm hành khách
                        string insertPassenger = @"
                            INSERT INTO Passenger
                            (BookingID, FullName, PassengerType, DocumentType, DocumentNumber)
                            OUTPUT INSERTED.PassengerID
                            VALUES
                            (@BookingID, @FullName, @PassengerType, @DocumentType, @DocumentNumber)";

                        SqlCommand cmdPassenger = new SqlCommand(insertPassenger, conn, trans);
                        cmdPassenger.Parameters.AddWithValue("@BookingID", bookingId);
                        cmdPassenger.Parameters.AddWithValue("@FullName", p.FullName);
                        cmdPassenger.Parameters.AddWithValue("@PassengerType", p.PassengerType);
                        cmdPassenger.Parameters.AddWithValue("@DocumentType", (object)p.DocumentType ?? DBNull.Value);
                        cmdPassenger.Parameters.AddWithValue("@DocumentNumber", (object)p.DocumentNumber ?? DBNull.Value);

                        int passengerId = (int)cmdPassenger.ExecuteScalar();

                        // 3. lấy PriceID theo Flight + SeatClass
                        string getPriceId = @"
                            SELECT TOP 1 PriceID
                            FROM FlightPrice
                            WHERE FlightID = @FlightID
                              AND SeatClass = @SeatClass";

                        SqlCommand cmdPrice = new SqlCommand(getPriceId, conn, trans);
                        cmdPrice.Parameters.AddWithValue("@FlightID", request.FlightID);
                        cmdPrice.Parameters.AddWithValue("@SeatClass", p.SeatClass);

                        object priceIdObj = cmdPrice.ExecuteScalar();
                        if (priceIdObj == null)
                            throw new Exception("Không tìm thấy giá vé cho hạng ghế " + p.SeatClass);

                        int priceId = Convert.ToInt32(priceIdObj);

                        // 4. tạo ticket tạm
                        string insertTicket = @"
                            INSERT INTO Ticket
                            (PassengerID, FlightID, PriceID, SeatID, TicketStatus)
                            VALUES
                            (@PassengerID, @FlightID, @PriceID, @SeatID, 'Pending')";

                        SqlCommand cmdTicket = new SqlCommand(insertTicket, conn, trans);
                        cmdTicket.Parameters.AddWithValue("@PassengerID", passengerId);
                        cmdTicket.Parameters.AddWithValue("@FlightID", request.FlightID);
                        cmdTicket.Parameters.AddWithValue("@PriceID", priceId);
                        cmdTicket.Parameters.AddWithValue("@SeatID", p.SeatID);

                        cmdTicket.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return bookingCode;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }
    }
}
