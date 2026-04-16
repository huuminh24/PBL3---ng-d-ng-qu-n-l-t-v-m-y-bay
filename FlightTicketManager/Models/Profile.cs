using System;
using System.Collections.Generic;

namespace FlightTicketManager.Models;

public partial class Profile
{
    public int ProfileId { get; set; }

    public int AccountId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Booking> BookingCreatedByProfiles { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingCustomerProfiles { get; set; } = new List<Booking>();
}
