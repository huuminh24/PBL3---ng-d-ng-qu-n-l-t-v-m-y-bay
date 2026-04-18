using FlightTicketManager.DAL;
using FlightTicketManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManager.BLL
{
    public class FlightBLL
    {
        private readonly FlightDAL _dal = new FlightDAL();

        public List<FlightDTO> GetFlights(string from, string to, DateTime? date, int passengerCount)
        {
            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
                throw new Exception("Điểm đi và điểm đến không được để trống!");

            if (from.Trim().ToUpper() == to.Trim().ToUpper())
                throw new Exception("Điểm đi và điểm đến không được trùng nhau!");

            DateTime searchDate = date ?? DateTime.Now;

            if (searchDate.Date < DateTime.Now.Date)
                throw new Exception("Không thể tìm chuyến bay trong quá khứ!");

            if (passengerCount < 1 || passengerCount > 9)
                throw new Exception("Số lượng hành khách phải từ 1 đến 9!");

            return _dal.SearchFlights(from, to, searchDate, passengerCount);
        }
    }
}
