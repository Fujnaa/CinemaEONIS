using Microsoft.EntityFrameworkCore;

namespace CinemaBackend.Services
{
    public class ScreeningService : IScreeningService
    {
        CinemaDatabaseContext _dbContext;

        public ScreeningService(CinemaDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Screening>> GetScreenings()
        {
            try
            {
                return await _dbContext.Screenings.Include(s => s.Tickets).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Screening> GetScreeningById(Guid screeningId)
        {
            try
            {
                Screening? search = await _dbContext.Screenings.Include(s => s.Tickets).FirstOrDefaultAsync(w => w.ScreeningId == screeningId);

                if (search == null)
                    throw new KeyNotFoundException();

                return search;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Screening> GetScreeningByDate(DateOnly screeningDate)
        {
            try
            {
                Screening? search = await _dbContext.Screenings.Include(s => s.Tickets).FirstOrDefaultAsync(w => w.ScreeningDate == screeningDate);

                if (search == null)
                    return null!;

                return search;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Screening> CreateScreening(Screening screening)
        {
            var createdScreening = await _dbContext.AddAsync(screening);

            await _dbContext.SaveChangesAsync();

            return createdScreening.Entity;
        }

        public async Task<Screening> UpdateScreening(Screening screening)
        {
            try
            {
                var toUpdate = await _dbContext.Screenings.FirstOrDefaultAsync(w => w.ScreeningId == screening.ScreeningId);

                if (toUpdate == null)
                    throw new KeyNotFoundException();

                toUpdate.ScreeningId = screening.ScreeningId;
                toUpdate.ScreeningDate = screening.ScreeningDate;
                toUpdate.ScreeningRoom = screening.ScreeningRoom;
                toUpdate.ScreeningStart = screening.ScreeningStart;
                toUpdate.ScreeningEnd = screening.ScreeningEnd;
                toUpdate.MovieId = screening.MovieId;
                toUpdate.Tickets = screening.Tickets;

                await _dbContext.SaveChangesAsync();

                return toUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteScreening(Guid screeningId)
        {
            try
            {
                Screening? search = await _dbContext.Screenings.FirstOrDefaultAsync(w => w.ScreeningId == screeningId);

                if (search == null)
                    throw new KeyNotFoundException();

                _dbContext.Screenings.Remove(search);

                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}
