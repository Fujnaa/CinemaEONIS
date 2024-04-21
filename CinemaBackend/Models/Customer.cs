using System;
using System.Collections.Generic;

namespace CinemaBackend.Models;

public partial class Customer
{
    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerEmailAdress { get; set; } = null!;

    public string CustomerPasswordHash { get; set; } = null!;

    public string? CustomerPhoneNumber { get; set; }

    public string CustomerMembershipLevel { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
