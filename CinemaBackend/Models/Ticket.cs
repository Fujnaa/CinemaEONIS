using System;
using System.Collections.Generic;

namespace CinemaBackend.Models;

public partial class Ticket
{
    public Guid TicketId { get; set; }

    public string TicketType { get; set; } = null!;

    public string TicketSeat { get; set; } = null!;

    public decimal TicketPrice { get; set; }

    public Guid? ScreeningId { get; set; }

    public Guid? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Screening? Screening { get; set; }
}
