namespace CinemaBackend.Models.DTOs.TicketDTOs
{
    public class TicketDto
    {

        public string TicketType { get; set; } = null!;

        public string TicketSeat { get; set; } = null!;

        public decimal TicketPrice { get; set; }

        public virtual Customer? Customer { get; set; }


    }
}
