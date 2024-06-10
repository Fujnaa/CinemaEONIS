using AutoMapper;
using CinemaBackend.Models.DTOs.CustomerDTOs;

namespace CinemaBackend.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() {
        
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<CustomerAdminDto, Customer>().ReverseMap();
            CreateMap<CustomerCreateDto, Customer>().ReverseMap();
            CreateMap<CustomerUpdateDto, Customer>().ReverseMap();

        }

    }
}
