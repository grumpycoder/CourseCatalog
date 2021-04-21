using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities
{
    public class Tag : AuditableEntity
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
    }
}