using ManagerService.Models.shared;
using System.ComponentModel.DataAnnotations;

namespace ManagerService.Models.manager
{
    public class DocumentType
    {
        [Key]
        public int IdDocumentType { get; set; }

        [Required]
        [MaxLength(255)]
        public string DocumentName { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Навигационные свойства
        public ICollection<DocumentForSpecialization> DocumentForSpecialization { get; set; } = new List<DocumentForSpecialization>();
    }
}
