using AutoMapper;
using ManagerService.DTOs;
using ManagerService.Models.shared;

namespace ManagerService.Mappings
{
    public class SpecializationProfile : Profile
    {
        public SpecializationProfile()
        {
            CreateMap<Specialization, SpecializationDTO>();
        }
    }
}
