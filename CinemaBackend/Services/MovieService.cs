using Microsoft.EntityFrameworkCore;

namespace CinemaBackend.Services
{
    public class MovieService : IMovieService
    {
        CinemaDatabaseContext _dbContext;

        public MovieService(CinemaDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Movie>> GetMovies()
        {
            try
            {
                return await _dbContext.Movies.Include(m => m.Screenings).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Movie> GetMovieById(Guid movieId)
        {
            try
            {
                Movie? search = await _dbContext.Movies.FirstOrDefaultAsync(w => w.MovieId == movieId);

                if (search == null)
                    throw new KeyNotFoundException();

                return search;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Movie> GetMovieByTitle(String movieTitle)
        {
            try
            {
                Movie? search = await _dbContext.Movies.FirstOrDefaultAsync(w => w.MovieTitle == movieTitle);

                if (search == null)
                    throw new KeyNotFoundException();

                return search;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            var createdMovie = await _dbContext.AddAsync(movie);

            await _dbContext.SaveChangesAsync();

            return createdMovie.Entity;
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            try
            {
                var toUpdate = await _dbContext.Movies.FirstOrDefaultAsync(w => w.MovieId == movie.MovieId);

                if (toUpdate == null)
                    throw new KeyNotFoundException();

                toUpdate.MovieId = movie.MovieId;
                toUpdate.MovieTitle = movie.MovieTitle;
                toUpdate.MovieGenre = movie.MovieGenre;
                toUpdate.MovieDirector = movie.MovieDirector;
                toUpdate.MovieCast = movie.MovieCast;
                toUpdate.WorkerId = movie.WorkerId;

                await _dbContext.SaveChangesAsync();

                return toUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteMovie(Guid movieId)
        {
            try
            {
                Movie? search = await _dbContext.Movies.FirstOrDefaultAsync(w => w.MovieId == movieId);

                if (search == null)
                    throw new KeyNotFoundException();

                _dbContext.Movies.Remove(search);

                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
