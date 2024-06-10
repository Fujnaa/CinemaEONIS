using AutoMapper;
using CinemaBackend.Models.DTOs.ScreeningDTOs;

namespace CinemaBackend.Profiles
{
    public class ScreeningProfile : Profile
    {
        public ScreeningProfile() {
        
            CreateMap<ScreeningDto, Screening>().ReverseMap();
            CreateMap<ScreeningAdminDto, Screening>().ReverseMap();
            CreateMap<ScreeningCreateDto, Screening>().ReverseMap();
            CreateMap<ScreeningUpdateDto, Screening>().ReverseMap();

        }

    }
}
