namespace CourseCatalog.Domain.Entities
{
    public class CourseEndorsement
    {
        public int EndorsementId { get; set; }
        public int CourseId { get; set; }
        public Endorsement Endorsement { get; set; }
        public Course Course { get; set; }
    }
}