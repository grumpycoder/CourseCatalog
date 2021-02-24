using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities
{
    public class DraftDeliveryType : AuditableEntity
    {
        public int DraftDeliveryTypeId { get; set; }
        public int DraftId { get; set; }
        public int DeliveryTypeId { get; set; }
        public Draft Draft { get; set; }
        public DeliveryType DeliveryType { get; set; }
    }
}