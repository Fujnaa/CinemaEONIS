namespace CinemaBackend.Models.DTOs.CustomerDTOs
{
    public class CustomerCreateDto
    {
        public string CustomerName { get; set; } = null!;

        public string CustomerEmailAdress { get; set; } = null!;

        public string CustomerPasswordHash { get; set; } = null!;

        public string? CustomerPhoneNumber { get; set; }

        public string CustomerMembershipLevel { get; set; } = null!;

    }
}
