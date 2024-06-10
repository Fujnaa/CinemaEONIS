namespace CinemaBackend.Models.DTOs.ScreeningDTOs
{
    public class ScreeningAdminDto
    {
        public Guid ScreeningId { get; set; }

        public DateOnly ScreeningDate { get; set; }

        public string ScreeningRoom { get; set; } = null!;

        public TimeOnly? ScreeningStart { get; set; }

        public TimeOnly? ScreeningEnd { get; set; }

        public Guid? MovieId { get; set; }

    }
}
