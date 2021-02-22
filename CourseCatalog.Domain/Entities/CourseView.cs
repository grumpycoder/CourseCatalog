namespace CourseCatalog.Domain.Entities
{
    //HACK: Use view because of DevExpress DataGrid
    public class CourseView
    {
        public int CourseId { get; set; }
        public string CourseNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CipCode { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
        public string ActiveRange { get; set; }
        public string LowGrade { get; set; }
        public string HighGrade { get; set; }
        public string GradeRange { get; set; }
        public int? BeginSequence { get; set; }
        public int? EndSequence { get; set; }
        public string SequenceRange { get; set; }
        public string CourseLevel { get; set; }
        public string CourseLevelCode { get; set; }
        public string ScedIdentifier { get; set; }
        public string ScedCategory { get; set; }
        public string ScedCategoryCode { get; set; }
        public string Subject { get; set; }
        public decimal? CreditHours { get; set; }
        public string Status { get; set; }
        public bool IsRetired { get; set; }

    }
}
