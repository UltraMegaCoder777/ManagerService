namespace ManagerService.DTOs
{
    public class ScheduledPracticeDTO
    {
        public int IdScheduledPractice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public SpecializationDTO? Specialization { get; set; }
        public PracticeTypeDTO? PracticeType { get; set; }
    }
}
