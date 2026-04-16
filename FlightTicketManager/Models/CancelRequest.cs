using System;
using System.Collections.Generic;

namespace FlightTicketManager.Models;

public partial class CancelRequest
{
    public int CancelRequestId { get; set; }

    public int TicketId { get; set; }

    public string? Reason { get; set; }

    public decimal? RefundAmount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime RequestDate { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;
}
