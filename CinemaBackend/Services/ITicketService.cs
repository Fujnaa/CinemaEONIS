using CinemaBackend.Models;

namespace CinemaBackend.Services
{
    public interface ITicketService
    {

        Task<List<Ticket>> GetTickets();
        Task<List<Ticket>> GetTicketsAdmin();
        Task<Ticket> GetTicketById(Guid ticketId);
        Task<Ticket> CreateTicket(Ticket ticket);
        Task<Ticket> UpdateTicket(Ticket ticket);
        Task DeleteTicket(Guid ticketId);

    }
}
