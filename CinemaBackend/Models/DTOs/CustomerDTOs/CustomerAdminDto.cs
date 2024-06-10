namespace CinemaBackend.Models.DTOs.CustomerDTOs
{
    public class CustomerAdminDto
    {
        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; } = null!;

        public string CustomerEmailAdress { get; set; } = null!;

        public string? CustomerPhoneNumber { get; set; }

        public string CustomerMembershipLevel { get; set; } = null!;
    }
}
