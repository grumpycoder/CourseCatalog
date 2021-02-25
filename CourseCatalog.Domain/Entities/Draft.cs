using System.Collections.Generic;

namespace CourseCatalog.Domain.Entities
{
    public class Draft
    {
        public int DraftId { get; set; }
        public string CourseNumber { get; set; }
        public string CipCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CollegeCourseId { get; set; }
        public bool? IsLocallyEditable { get; set; }
        public bool? IsCareerTech { get; set; }
        public bool? IsSpecialEducation { get; set; }
        public bool IsCollege { get; set; }

        public string ScedCourseNumber { get; set; }
        public string StateAttribute1 { get; set; }
        public string StateAttribute2 { get; set; }

        public int? ScedCategoryId { get; set; }
        public ScedCategory ScedCategory { get; set; }

        public int GradeScaleId { get; set; }
        public GradeScale GradeScale { get; set; }

        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }

        public Grade LowGrade { get; set; }
        public int? LowGradeId { get; set; }

        public Grade HighGrade { get; set; }
        public int? HighGradeId { get; set; }

        public int? SubjectId { get; set; }
        public Subject Subject { get; set; }

        public decimal? CreditHours { get; set; }
        public int? BeginSequence { get; set; }
        public int? EndSequence { get; set; }

        public int? CourseLevelId { get; set; }
        public CourseLevel CourseLevel { get; set; }
        public List<string> CreditTypes { get; set; }
        public List<string> Tags { get; set; }

        public CourseStatus? Status { get; set; }

        public List<ProgramDraft> Programs { get; set; } = new List<ProgramDraft>();

        public List<DraftDeliveryType> DeliveryTypes { get; set; } = new List<DraftDeliveryType>();

        public List<DraftEndorsement> Endorsements { get; set; } = new List<DraftEndorsement>();

        //public DateTime LastUpdate { get; set; }
    }


}
