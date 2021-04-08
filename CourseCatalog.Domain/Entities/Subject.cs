using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities
{
    public class Subject : AuditableEntity
    {
        public int SubjectId { get; set; }
        public string SubjectCode { get; set; }
        public string Name { get; set; }
    }
}