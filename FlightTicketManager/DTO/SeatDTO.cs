using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManager.DTO
{
    public class SeatDTO
    {
        public int SeatID { get; set; }
        public int FlightID { get; set; }
        public string SeatNumber { get; set; } = "";
        public string SeatClass { get; set; } = "";
        public string SeatStatus { get; set; } = "";
    }
}
