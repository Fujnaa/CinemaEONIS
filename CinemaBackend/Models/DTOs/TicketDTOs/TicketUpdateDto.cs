namespace CinemaBackend.Models.DTOs.TicketDTOs
{
    public class TicketUpdateDto
    {
        public Guid TicketId { get; set; }

        public string TicketType { get; set; } = null!;

        public string TicketSeat { get; set; } = null!;

        public decimal TicketPrice { get; set; }

        public Guid? ScreeningId { get; set; }

        public Guid? CustomerId { get; set; }

    }
}
