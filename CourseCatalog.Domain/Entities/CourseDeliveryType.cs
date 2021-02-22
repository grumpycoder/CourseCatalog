namespace CourseCatalog.Domain.Entities
{
    public class CourseDeliveryType
    {
        public int CourseDeliveryTypeId { get; set; }
        public int CourseId { get; set; }
        public int DeliveryTypeId { get; set; }
        public Course Course { get; set; }
        public DeliveryType DeliveryType { get; set; }
    }
}