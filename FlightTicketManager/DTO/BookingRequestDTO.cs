using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManager.DTO
{
    public class BookingRequestDTO
    {
        public int CustomerProfileID { get; set; }
        public int CreatedByProfileID { get; set; }
        public int FlightID { get; set; }

        public string ContactFullName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }

        public decimal OriginalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }

        public DateTime HoldExpiredAt { get; set; }

        public List<PassengerDTO> Passengers { get; set; } = new List<PassengerDTO>();
    }
}
