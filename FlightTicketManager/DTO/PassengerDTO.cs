using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManager.DTO
{
    public class PassengerDTO
    {
        public string FullName { get; set; }
        public string PassengerType { get; set; }   // Adult, Child, Infant
        public string DocumentType { get; set; }    // CCCD, Passport
        public string DocumentNumber { get; set; }
        public string Nationality { get; set; }     // chỉ dùng tạm ở UI, DB chưa có cột này
        public DateTime DateOfBirth { get; set; }   // chỉ dùng tạm ở UI, DB chưa có cột này
        public string Phone { get; set; }           // contact chung
        public string Email { get; set; }           // contact chung
        public int SeatID { get; set; }
        public string SeatClass { get; set; }
    }
}
