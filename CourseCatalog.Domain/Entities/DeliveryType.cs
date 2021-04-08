using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities
{
    public class DeliveryType: AuditableEntity
    {
        public int DeliveryTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}