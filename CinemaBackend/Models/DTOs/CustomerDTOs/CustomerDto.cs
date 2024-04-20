namespace CinemaBackend.Models.DTOs.CustomerDTOs
{
    public class CustomerDto
    {

        public string CustomerName { get; set; } = null!;

        public string CustomerEmailAdress { get; set; } = null!;

        public string? CustomerPhoneNumber { get; set; }

        public string CustomerMembershipLevel { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
