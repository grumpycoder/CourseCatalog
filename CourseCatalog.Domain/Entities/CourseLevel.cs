using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities
{
    public class CourseLevel : AuditableEntity
    {
        public int CourseLevelId { get; set; }
        public string CourseLevelCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}