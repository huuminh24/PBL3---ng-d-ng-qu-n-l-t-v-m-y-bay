using System;
using System.Collections.Generic;

namespace FlightTicketManager.Models;

public partial class Passenger
{
    public int PassengerId { get; set; }

    public int BookingId { get; set; }

    public string FullName { get; set; } = null!;

    public string PassengerType { get; set; } = null!;

    public string? DocumentType { get; set; }

    public string? DocumentNumber { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
