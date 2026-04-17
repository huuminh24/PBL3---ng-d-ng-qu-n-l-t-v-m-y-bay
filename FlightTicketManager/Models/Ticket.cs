using System;
using System.Collections.Generic;

namespace FlightTicketManager.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int PassengerId { get; set; }

    public int FlightId { get; set; }

    public int PriceId { get; set; }

    public int SeatId { get; set; }

    public string TicketStatus { get; set; } = null!;

    public virtual CancelRequest? CancelRequest { get; set; }

    public virtual Flight Flight { get; set; } = null!;

    public virtual Passenger Passenger { get; set; } = null!;

    public virtual FlightPrice Price { get; set; } = null!;

    public virtual Seat Seat { get; set; } = null!;
}
