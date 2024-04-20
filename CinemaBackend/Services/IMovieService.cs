using CinemaBackend.Models;

namespace CinemaBackend.Services
{
    public interface IMovieService
    {

        Task<List<Movie>> GetMovies();
        Task<Movie> GetMovieById(Guid movieId);
        Task<Movie> CreateMovie(Movie movie);
        Task<Movie> UpdateMovie(Movie movie);
        Task DeleteMovie(Guid movieId);

    }
}
