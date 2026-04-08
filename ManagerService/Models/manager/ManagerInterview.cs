using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerService.Models.manager
{
    public class ManagerInterview
    {
        [Key]
        [ForeignKey(nameof(ManagerSlot))]
        public int IdManagerSlot { get; set; }  // PK и FK одновременно (1:1)

        [Required]
        public int IdStudent { get; set; } // fk for student

        [Required]
        public int IdStudentApplication { get; set; } // fk for student application

        [Required]
        public bool Result { get; set; }  // true - принят, false - отклонён

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Навигационные свойства
        public ManagerSlot? ManagerSlot { get; set; }
    }
}
