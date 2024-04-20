using CinemaBackend.Models;

namespace CinemaBackend.Services
{
    public interface IScreeningService
    {

        Task<List<Screening>> GetScreenings();
        Task<Screening> GetScreeningById(Guid screeningId);
        Task<Screening> CreateScreening(Screening screening);
        Task<Screening> UpdateScreening(Screening screening);
        Task DeleteScreening(Guid screeningId);

    }
}
