namespace CourseCatalog.Domain.Entities
{
    public class ProgramCourse
    {
        public int CourseId { get; set; }
        public int ProgramId { get; set; }
        public Course Course { get; set; }
        public Program Program { get; set; }
    }
}