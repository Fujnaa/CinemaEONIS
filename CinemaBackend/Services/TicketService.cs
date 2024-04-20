using Microsoft.EntityFrameworkCore;

namespace CinemaBackend.Services
{
    public class TicketService : ITicketService
    {

        CinemaDatabaseContext _dbContext;

        public TicketService(CinemaDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Ticket>> GetTickets()
        {
            try
            {
                return await _dbContext.Tickets.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Ticket> GetTicketById(Guid ticketId)
        {
            try
            {
                Ticket? search = await _dbContext.Tickets.FirstOrDefaultAsync(w => w.TicketId == ticketId);

                if (search == null)
                    throw new KeyNotFoundException();

                return search;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Ticket> CreateTicket(Ticket ticket)
        {
            var createdTicket = await _dbContext.AddAsync(ticket);

            await _dbContext.SaveChangesAsync();

            return createdTicket.Entity;
        }

        public async Task<Ticket> UpdateTicket(Ticket ticket)
        {
            try
            {
                var toUpdate = await _dbContext.Tickets.FirstOrDefaultAsync(w => w.TicketId == ticket.TicketId);

                if (toUpdate == null)
                    throw new KeyNotFoundException();

                toUpdate.TicketId = ticket.TicketId;
                toUpdate.TicketType = ticket.TicketType;
                toUpdate.TicketSeat = ticket.TicketSeat;
                toUpdate.TicketPrice = ticket.TicketPrice;
                toUpdate.ScreeningId = ticket.ScreeningId;
                toUpdate.CustomerId = ticket.CustomerId;

                await _dbContext.SaveChangesAsync();

                return toUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteTicket(Guid ticketId)
        {
            try
            {
                Ticket? search = await _dbContext.Tickets.FirstOrDefaultAsync(w => w.TicketId == ticketId);

                if (search == null)
                    throw new KeyNotFoundException();

                _dbContext.Tickets.Remove(search);

                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}
