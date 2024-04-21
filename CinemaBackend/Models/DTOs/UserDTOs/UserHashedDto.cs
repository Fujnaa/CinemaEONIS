namespace CinemaBackend.Models.DTOs.UserDTOs
{
    public class UserHashedDto
    {
        public string UserName { get; set; } = null!;

        public string UserEmailAdress { get; set; } = null!;

        public string UserPhoneNumber { get; set; } = null!;

        public string UserPasswordHash { get; set; } = null!;

    }
}
