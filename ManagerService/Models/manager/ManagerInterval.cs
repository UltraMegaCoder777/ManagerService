using ManagerService.Models.shared;
using System.ComponentModel.DataAnnotations;

namespace ManagerService.Models.manager
{
    public class ManagerInterval
    {
        [Key]
        public int IdManagerInterval { get; set; }

        [Required]
        public int IdEmployee { get; set; }  // Кто будет проводить собеседования

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int MaxCount { get; set; }  // Максимум студентов в этот интервал

        public TimeSpan? BreakDuration { get; set; }  // Перерыв между слотами

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public ICollection<ManagerSlot> ManagerSlots { get; set; } = new List<ManagerSlot>();

        public Employee manager { get; set; } = new Employee();
    }
}
