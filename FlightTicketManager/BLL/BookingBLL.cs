using FlightTicketManager.DAL;
using FlightTicketManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManager.BLL
{
    public class BookingBLL
    {
        private readonly BookingDAL _dal = new BookingDAL();

        public string CreateBooking(BookingRequestDTO request)
        {
            if (request == null)
                throw new Exception("Dữ liệu đặt vé không hợp lệ.");

            if (request.Passengers == null || request.Passengers.Count == 0)
                throw new Exception("Phải có ít nhất 1 hành khách.");

            if (request.Passengers.Count > 9)
                throw new Exception("Tối đa 9 hành khách cho một lần đặt vé");

            if (string.IsNullOrWhiteSpace(request.ContactFullName))
                throw new Exception("Tên người liên hệ không được để trống.");

            if (string.IsNullOrWhiteSpace(request.ContactPhone))
                throw new Exception("Số điện thoại liên hệ không được để trống.");

            foreach (var p in request.Passengers)
            {
                if (string.IsNullOrWhiteSpace(p.FullName))
                    throw new Exception("Họ tên hành khách không được để trống.");

                if (p.SeatID <= 0)
                    throw new Exception("Mỗi hành khách phải chọn 1 ghế.");

                if (string.IsNullOrWhiteSpace(p.SeatClass))
                    throw new Exception("Thiếu hạng ghế.");
            }

            return _dal.CreateBooking(request);
        }
    }
}
