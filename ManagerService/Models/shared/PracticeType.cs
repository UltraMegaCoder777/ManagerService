using System.ComponentModel.DataAnnotations;

namespace ManagerService.Models.shared;

public class PracticeType
{
    [Key]
    public int IdPracticeType { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    // Навигационные свойства
    public ICollection<ScheduledPractice> ScheduledPractices { get; set; } = new List<ScheduledPractice>();
}