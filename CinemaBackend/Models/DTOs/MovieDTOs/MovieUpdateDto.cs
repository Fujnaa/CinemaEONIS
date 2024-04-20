namespace CinemaBackend.Models.DTOs.MovieDTOs
{
    public class MovieUpdateDto
    {
        public Guid MovieId { get; set; }

        public string MovieTitle { get; set; } = null!;

        public string? MovieGenre { get; set; }

        public string? MovieDirector { get; set; }

        public string? MovieCast { get; set; }

        public Guid? WorkerId { get; set; }

    }
}
