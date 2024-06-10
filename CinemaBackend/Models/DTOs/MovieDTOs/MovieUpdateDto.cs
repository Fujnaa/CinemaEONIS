namespace CinemaBackend.Models.DTOs.MovieDTOs
{
    public class MovieUpdateDto
    {
        public Guid MovieId { get; set; }

        public string MovieTitle { get; set; } = null!;

        public string? MovieDesc { get; set; }

        public string? MovieGenre { get; set; }

        public string? MovieDirector { get; set; }

        public DateOnly? MovieReleaseDate { get; set; }

        public decimal? MovieLength { get; set; }

        public string? MovieCast { get; set; }

        public string? MoviePoster { get; set; }

        public string? MovieTrailer { get; set; }

        public int? SecondsToSkipTrailer { get; set; }

        public Guid? WorkerId { get; set; }

    }
}
