using AutoMapper;
using CinemaBackend.Models.DTOs.MovieDTOs;

namespace CinemaBackend.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile() {

            CreateMap<MovieDto, Movie>().ReverseMap();
            CreateMap<MovieAdminDto, Movie>().ReverseMap();
            CreateMap<MovieCreateDto, Movie>().ReverseMap();
            CreateMap<MovieUpdateDto, Movie>().ReverseMap();

        }
    }
}
