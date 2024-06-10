using CinemaBackend.Models.DTOs.TicketDTOs;

namespace CinemaBackend.Models.DTOs.ScreeningDTOs
{
    public class ScreeningDto
    {

        public DateOnly ScreeningDate { get; set; }

        public string ScreeningRoom { get; set; } = null!;

        public TimeOnly? ScreeningStart { get; set; }

        public TimeOnly? ScreeningEnd { get; set; }

        public virtual ICollection<TicketDto> Tickets { get; set; } = new List<TicketDto>();

    }
}
