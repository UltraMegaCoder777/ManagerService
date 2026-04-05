using ManagerService.Models.shared;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace ManagerService.Models.manager
{
    [PrimaryKey(nameof(IdDocumentType), nameof(IdSpecialization))]
    public class DocumentForSpecialization
    {
        // [Key]
        public int IdDocumentType { get; set; }

        // [Key]
        public int IdSpecialization { get; set; }

        public ICollection<StudentDocument> StudentDocument { get; set; } = new List<StudentDocument>(); // Навигационное свойство
    }
}
