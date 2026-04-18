using FlightTicketManager.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace FlightTicketManager.DAL
{
    public class FlightDAL
    {
        private readonly string connString =
            ConfigurationManager.ConnectionStrings["FlightDbConn"].ConnectionString;

        public List<FlightDTO> SearchFlights(string from, string to, DateTime date, int passengerCount)
        {
            List<FlightDTO> list = new List<FlightDTO>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"
                    SELECT 
                        f.FlightID,
                        f.FlightCode,
                        f.AirlineCode,
                        f.AirlineName,
                        f.DepartureCode,
                        f.DepartureLocation,
                        f.ArrivalCode,
                        f.ArrivalLocation,
                        f.DepartureTime,
                        f.ArrivalTime,
                        f.Status,
                        ISNULL(MAX(CASE WHEN fp.SeatClass = 'Economy' THEN fp.Price END), 0) AS EconomyPrice,
                        ISNULL(MAX(CASE WHEN fp.SeatClass = 'Business' THEN fp.Price END), 0) AS BusinessPrice,
                        ISNULL(SUM(CASE WHEN s.SeatStatus = 'Available' THEN 1 ELSE 0 END), 0) AS AvailableSeats
                    FROM Flight f
                    LEFT JOIN FlightPrice fp ON f.FlightID = fp.FlightID
                    LEFT JOIN Seat s ON f.FlightID = s.FlightID
                    WHERE f.DepartureCode = @from
                      AND f.ArrivalCode = @to
                      AND CAST(f.DepartureTime AS DATE) = @date
                      AND f.Status = 'Scheduled'
                    GROUP BY
                        f.FlightID, f.FlightCode, f.AirlineCode, f.AirlineName,
                        f.DepartureCode, f.DepartureLocation,
                        f.ArrivalCode, f.ArrivalLocation,
                        f.DepartureTime, f.ArrivalTime, f.Status
                    HAVING ISNULL(SUM(CASE WHEN s.SeatStatus = 'Available' THEN 1 ELSE 0 END), 0) >= @passengerCount
                    ORDER BY f.DepartureTime";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@from", from.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@to", to.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@date", date.Date);
                cmd.Parameters.AddWithValue("@passengerCount", passengerCount);

                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    list.Add(new FlightDTO
                    {
                        FlightID = Convert.ToInt32(r["FlightID"]),
                        FlightCode = r["FlightCode"].ToString(),
                        AirlineCode = r["AirlineCode"].ToString(),
                        AirlineName = r["AirlineName"].ToString(),
                        DepartureCode = r["DepartureCode"].ToString(),
                        DepartureLocation = r["DepartureLocation"].ToString(),
                        ArrivalCode = r["ArrivalCode"].ToString(),
                        ArrivalLocation = r["ArrivalLocation"].ToString(),
                        DepartureTime = Convert.ToDateTime(r["DepartureTime"]),
                        ArrivalTime = Convert.ToDateTime(r["ArrivalTime"]),
                        Status = r["Status"].ToString(),
                        EconomyPrice = Convert.ToDecimal(r["EconomyPrice"]),
                        BusinessPrice = Convert.ToDecimal(r["BusinessPrice"]),
                        AvailableSeats = Convert.ToInt32(r["AvailableSeats"])
                    });
                }
            }

            return list;
        }
    }
}
