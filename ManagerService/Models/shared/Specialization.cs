using ManagerService.Models.manager;
using System.ComponentModel.DataAnnotations;

namespace ManagerService.Models.shared;

public class Specialization
{
    [Key]
    public int IdSpecialization { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    // Навигационные свойства
    public ICollection<ScheduledPractice> ScheduledPractices { get; set; } = new List<ScheduledPractice>();
    public ICollection<DocumentForSpecialization> DocumentForSpecialization { get; set; } = new List<DocumentForSpecialization>();
}