using AutoMapper;
using ManagerService.DTOs;
using ManagerService.Models.shared;

namespace ManagerService.Mappings
{
    public class ScheduledPracticeProfile : Profile
    {
        public ScheduledPracticeProfile()
        {
            // Маппинг Specialization → SpecializationDTO
            CreateMap<Specialization, SpecializationDTO>();

            // Маппинг PracticeType → PracticeTypeDTO
            CreateMap<PracticeType, PracticeTypeDTO>();

            // Маппинг ScheduledPractice → ScheduledPracticeDTO
            CreateMap<ScheduledPractice, ScheduledPracticeDTO>()
                .ForMember(dest => dest.Specialization,
                    opt => opt.MapFrom(src => src.Specialization))
                .ForMember(dest => dest.PracticeType,
                    opt => opt.MapFrom(src => src.PracticeType));
        }
    }
}