using System;
using System.Collections.Generic;

namespace FlightTicketManager.Models;

public partial class Seat
{
    public int SeatId { get; set; }

    public int FlightId { get; set; }

    public string SeatNumber { get; set; } = null!;

    public string SeatClass { get; set; } = null!;

    public string SeatStatus { get; set; } = null!;

    public virtual Flight Flight { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
