using AutoMapper;
using CinemaBackend.Models.DTOs.TicketDTOs;

namespace CinemaBackend.Profiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile() {
        
            CreateMap<TicketDto, Ticket>().ReverseMap();
            CreateMap<TicketAdminDto, Ticket>().ReverseMap();
            CreateMap<TicketCreateDto, Ticket>().ReverseMap();
            CreateMap<TicketUpdateDto, Ticket>().ReverseMap();

        }
    }
}
