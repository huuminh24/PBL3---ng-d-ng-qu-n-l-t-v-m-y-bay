using System;
using System.Collections.Generic;

namespace FlightTicketManager.Models;

public partial class FlightPrice
{
    public int PriceId { get; set; }

    public int FlightId { get; set; }

    public string SeatClass { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual Flight Flight { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
