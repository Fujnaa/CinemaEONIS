using System;
using System.Collections.Generic;

namespace CinemaBackend.Models;

public partial class Screening
{
    public Guid ScreeningId { get; set; }

    public DateOnly ScreeningDate { get; set; }

    public string ScreeningRoom { get; set; } = null!;

    public TimeOnly? ScreeningStart { get; set; }

    public TimeOnly? ScreeningEnd { get; set; }

    public Guid? MovieId { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
