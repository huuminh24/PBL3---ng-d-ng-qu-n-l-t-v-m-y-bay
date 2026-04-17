using System;
using System.Collections.Generic;

namespace FlightTicketManager.Models;

public partial class Flight
{
    public int FlightId { get; set; }

    public string FlightCode { get; set; } = null!;

    public string AirlineCode { get; set; } = null!;

    public string AirlineName { get; set; } = null!;

    public string DepartureCode { get; set; } = null!;

    public string DepartureLocation { get; set; } = null!;

    public string ArrivalCode { get; set; } = null!;

    public string ArrivalLocation { get; set; } = null!;

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<FlightPrice> FlightPrices { get; set; } = new List<FlightPrice>();

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
