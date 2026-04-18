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
    public class SeatDAL
    {
        private readonly string connString =
            ConfigurationManager.ConnectionStrings["FlightDbConn"].ConnectionString;

        public List<SeatDTO> GetSeatsByFlight(int flightId)
        {
            List<SeatDTO> list = new List<SeatDTO>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"
                    SELECT SeatID, FlightID, SeatNumber, SeatClass, SeatStatus
                    FROM Seat
                    WHERE FlightID = @flightId
                    ORDER BY TRY_CAST(SeatNumber AS INT), SeatNumber";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@flightId", flightId);

                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    list.Add(new SeatDTO
                    {
                        SeatID = (int)r["SeatID"],
                        FlightID = (int)r["FlightID"],
                        SeatNumber = r["SeatNumber"].ToString() ?? "",
                        SeatClass = r["SeatClass"].ToString() ?? "",
                        SeatStatus = r["SeatStatus"].ToString() ?? ""
                    });
                }
            }

            return list;
        }
    }
}
