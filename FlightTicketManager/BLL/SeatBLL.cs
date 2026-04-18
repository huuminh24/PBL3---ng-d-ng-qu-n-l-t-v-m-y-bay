using FlightTicketManager.DAL;
using FlightTicketManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManager.BLL
{
    public class SeatBLL
    {
        private readonly SeatDAL _dal = new SeatDAL();

        public List<SeatDTO> GetSeatsByFlight(int flightId)
        {
            return _dal.GetSeatsByFlight(flightId);
        }

    }
}
