using AutoMapper;
using ManagerService.DTOs;
using ManagerService.Models.shared;

namespace ManagerService.Mappings
{
    public class ScheduledPracticeProfile : Profile
    {
        public ScheduledPracticeProfile()
        {
            CreateMap<ScheduledPractice, ScheduledPracticeDTO>()
                .ForMember(dest => dest.Specialization,
                    opt => opt.MapFrom(src => src.Specialization))
                .ForMember(dest => dest.PracticeType,
                    opt => opt.MapFrom(src => src.PracticeType));
        }
    }
}