using System;
using System.Collections.Generic;

namespace CinemaBackend.Models;

public partial class Worker
{
    public Guid WorkerId { get; set; }

    public string WorkerName { get; set; } = null!;

    public string WorkerEmailAdress { get; set; } = null!;

    public string WorkerPasswordHash { get; set; } = null!;

    public string WorkerPhoneNumber { get; set; } = null!;

    public decimal? WorkerSalary { get; set; }

    public string? WorkerCity { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
