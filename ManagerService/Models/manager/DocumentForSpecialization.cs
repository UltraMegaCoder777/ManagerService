using ManagerService.Models.shared;
using Microsoft.EntityFrameworkCore;

namespace ManagerService.Models.manager
{
    [PrimaryKey(nameof(IdDocumentType), nameof(IdSpecialization))]
    public class DocumentForSpecialization
    {
        // [Key]
        public int IdDocumentType { get; set; }

        // [Key]
        public int IdSpecialization { get; set; }

        // navigation
        public DocumentType? DocumentType { get; set; } = new DocumentType();
        public Specialization? Specialization { get; set; } = new Specialization();
    }
}
