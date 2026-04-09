using AutoMapper;
using ManagerService.DTOs;
using ManagerService.Models.shared;

namespace ManagerService.Mappings
{
    public class PracticeTypeProfile : Profile
    {
        public PracticeTypeProfile()
        {
            CreateMap<PracticeType, PracticeTypeDTO>();
        }
    }
}
