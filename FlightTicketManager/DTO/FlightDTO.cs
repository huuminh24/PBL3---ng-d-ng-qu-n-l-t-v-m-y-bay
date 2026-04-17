using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManager.DTO
{
    public class FlightDTO
    {
        public int FlightID { get; set; }
        public string FlightCode { get; set; }
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public string DepartureCode { get; set; }
        public string DepartureLocation { get; set; }
        public string ArrivalCode { get; set; }
        public string ArrivalLocation { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Status { get; set; }
    }
}
