using System;
using System.Collections.Generic;

namespace FlightTicketManager.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public string BookingCode { get; set; } = null!;

    public int? CustomerProfileId { get; set; }

    public int CreatedByProfileId { get; set; }

    public int? PromotionId { get; set; }

    public string BookingChannel { get; set; } = null!;

    public string BookingStatus { get; set; } = null!;

    public string? ContactFullName { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public DateTime? HoldExpiredAt { get; set; }

    public decimal OriginalAmount { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal FinalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Profile CreatedByProfile { get; set; } = null!;

    public virtual Profile? CustomerProfile { get; set; }

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();

    public virtual Payment? Payment { get; set; }

    public virtual Promotion? Promotion { get; set; }
}
