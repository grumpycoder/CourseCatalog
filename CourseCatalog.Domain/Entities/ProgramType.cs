using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities
{
    public class ProgramType : AuditableEntity
    {
        public int ProgramTypeId { get; set; }
        public string Name { get; set; }
        public string ProgramTypeCode { get; set; }
        public string Description { get; set; }
    }
}