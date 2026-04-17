using System;
using System.Collections.Generic;

namespace FlightTicketManager.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Profile? Profile { get; set; }

    public virtual Role Role { get; set; } = null!;
}
