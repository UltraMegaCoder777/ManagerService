using ManagerService.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerService.Models.manager
{
    public class ManagerSlot
    {
        [Key]
        public int IdManagerSlot { get; set; }

        [Required]
        public int IdEmployee { get; set; }  // Кто будет проводить собеседование

        public int IdManagerInterval { get; set; }  // FK на TimeInterval

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Column(TypeName = "manager_slot_status")]
        public ManagerSlotStatus Status { get; set; } = ManagerSlotStatus.Черновик;

        [MaxLength(255)]
        public string? MeetingPlace { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Навигационные свойства
        [ForeignKey(nameof(IdManagerInterval))]
        public ManagerInterval? ManagerInterval { get; set; }

        public ManagerInterview? ManagerInterview { get; set; }  // 1:1 связь
    }
}
