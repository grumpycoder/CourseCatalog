using System.Collections.Generic;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.App.Features.Drafts.Queries.GetDraftDetail
{
    public class DraftDetailDto
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
        public List<string> CreditTypes { get; set; }
        public List<string> Tags { get; set; }

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
        public List<DraftDeliveryTypeDto> DeliveryTypes { get; set; }
        public List<DraftEndorsementDto> Endorsements { get; set; }

        public List<ProgramDraftDto> Programs { get; set; }

    }

    public class ProgramDraftDto
    {
        public int ProgramCourseId { get; set; }
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
    }

    public class DraftEndorsementDto
    {
        public int CourseEndorsementId { get; set; }
        public int EndorsementId { get; set; }
        public string EndorseCode { get; set; }
        public string Description { get; set; }
    }

    public class DraftDeliveryTypeDto
    {
        public int CourseDeliveryTypeId { get; set; }
        public int DeliveryTypeId { get; set; }
        public string DeliveryTypeName { get; set; }
    }
}