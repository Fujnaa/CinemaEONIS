using AutoMapper;
using CinemaBackend.Models.DTOs.WorkerDTOs;

namespace CinemaBackend.Profiles
{
    public class WorkerProfile : Profile
    {
        public WorkerProfile() {

            CreateMap<WorkerDto, Worker>().ReverseMap();
            CreateMap<WorkerAdminDto, Worker>().ReverseMap();
            CreateMap<WorkerCreateDto, Worker>().ReverseMap();
            CreateMap<WorkerUpdateDto, Worker>().ReverseMap();

        }
    }
}
