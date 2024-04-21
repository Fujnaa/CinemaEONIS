using AutoMapper;
using CinemaBackend.Models;
using CinemaBackend.Models.DTOs.CustomerDTOs;
using CinemaBackend.Models.DTOs.MovieDTOs;
using CinemaBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private IMovieService _movieService;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieDto>>> GetMovies()
        {
            try
            {
                List<Movie> movies = await _movieService.GetMovies();

                if (movies == null || movies.Count == 0)
                    return NoContent();

                List<MovieDto> moviesDto = new List<MovieDto>();

                foreach (var movie in movies)
                {

                    MovieDto movieDto = _mapper.Map<MovieDto>(movie);
                    moviesDto.Add(movieDto);

                }

                return Ok(moviesDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }

        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieDto>> GetMovieById(Guid movieId)
        {
            try
            {
                Movie movie = await _movieService.GetMovieById(movieId);

                if (movie == null)
                    return NotFound();

                MovieDto movieDto = _mapper.Map<MovieDto>(movie);

                return Ok(movieDto);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("Title/{movieTitle}")]
        public async Task<ActionResult<MovieDto>> GetMovieByTitle(String movieTitle)
        {
            try
            {
                Movie movie = await _movieService.GetMovieByTitle(movieTitle);

                if (movie == null)
                    return NotFound();

                MovieDto movieDto = _mapper.Map<MovieDto>(movie);

                return Ok(movieDto);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost]
        [Authorize (Roles = "Worker")]
        public async Task<ActionResult<MovieDto>> CreateMovie(MovieCreateDto movie)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Movie toCreate = _mapper.Map<Movie>(movie);

                Movie createdMovie = await _movieService.CreateMovie(toCreate);

                return _mapper.Map<MovieDto>(createdMovie);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);

            }
        }

        [HttpPut]
        [Authorize(Roles = "Worker")]
        public async Task<ActionResult<MovieDto>> UpdateMovie(MovieUpdateDto movie)
        {
            try
            {
                Movie toUpdate = _mapper.Map<Movie>(movie);

                Movie updatedMovie = await _movieService.UpdateMovie(toUpdate);

                MovieDto updatedMovieDto = _mapper.Map<MovieDto>(updatedMovie);

                return Ok(updatedMovieDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Worker")]
        public async Task<ActionResult> DeleteMovie(Guid movieId)
        {
            try
            {

                await _movieService.DeleteMovie(movieId);
                return Ok();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);

            }
        }
    }

}
