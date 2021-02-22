using CourseCatalog.Domain.Entities;

namespace CourseCatalog.App.Features.Courses.Queries.GetCourseDetail
{
    public class CourseDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CourseNumber { get; set; }
        public string CollegeCourseId { get; set; }

        public string StateAttribute1 { get; set; }
        public string StateAttribute2 { get; set; }

        public string CipCode { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }

        public decimal? CreditHours { get; set; }
        public string CreditTypes { get; set; }
        public string Tags { get; set; }

        public Grade LowGrade { get; set; }
        public Grade HighGrade { get; set; }

        public bool? IsCareerTech { get; set; }
        public bool? IsSpecialEducation { get; set; }
        public bool? IsLocallyEditable { get; set; }
        public bool? IsCollege { get; set; }

        public Subject Subject { get; set; }

        public CourseLevel CourseLevel { get; set; }
        public GradeScale GradeScale { get; set; }
        public ScedCategory ScedCategory { get; set; }
    }
}
