using System.Collections.Generic;

namespace CourseCatalog.App.Features.Courses.Queries.GetCoursesByEndorsement
{
    public class EndorsementCourseListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseNumber { get; set; }
        public string Description { get; set; }

        public string CipCode { get; set; }
        public string CollegeCourseId { get; set; }
        public bool? IsLocallyEditable { get; set; }
        public bool? IsCareerTech { get; set; }
        public bool? IsSpecialEducation { get; set; }
        public bool IsCollege { get; set; }

        public string GradeScale { get; set; }
        public decimal? CreditHours { get; set; }

        public int BeginYear { get; set; }
        public int? EndYear { get; set; }

        public string ScedCourseNumber { get; set; }
        public string StateAttribute1 { get; set; }
        public string StateAttribute2 { get; set; }

        public string Subject { get; set; }
        public string LowGrade { get; set; }
        public string HighGrade { get; set; }
        public string ScedCategory { get; set; }

        public string CourseLevel { get; set; }

        public List<string> Tags { get; set; }
        public List<string> CreditTypes { get; set; }
        public string CourseStatus { get; set; }

    }

}
