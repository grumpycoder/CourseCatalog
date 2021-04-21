using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities
{
    public class ClusterType : AuditableEntity
    {
        public int ClusterTypeId { get; set; }
        public string Name { get; set; }
    }
}