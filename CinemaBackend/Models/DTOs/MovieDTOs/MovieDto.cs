namespace CinemaBackend.Models.DTOs.MovieDTOs
{
    public class MovieDto
    {
        public string MovieTitle { get; set; } = null!;

        public string? MovieGenre { get; set; }

        public string? MovieDirector { get; set; }

        public string? MovieCast { get; set; }

        public virtual ICollection<Screening> Screenings { get; set; } = new List<Screening>();

    }
}
