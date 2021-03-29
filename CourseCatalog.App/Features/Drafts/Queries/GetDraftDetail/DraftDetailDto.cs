using CourseCatalog.Domain.Entities;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Drafts.Queries.GetDraftDetail
{
    public class DraftDetailDto
    {
        public int DraftId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CourseNumber { get; set; }
        public string CollegeCourseId { get; set; }

        public string ScedCourseNumber { get; set; }
        public string StateAttribute1 { get; set; }
        public string StateAttribute2 { get; set; }

        public string CipCode { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }

        public decimal? CreditHours { get; set; }
        public List<string> CreditTypes { get; set; }
        public List<string> Tags { get; set; }

        public int? LowGradeId { get; set; }
        public Grade LowGrade { get; set; }
        public int? HighGradeId { get; set; }
        public Grade HighGrade { get; set; }

        public GradeRange GradeRange {
            get
            {
                var gr = new GradeRange()
                {
                    LowGrade = LowGrade.Name,
                    HighGrade = HighGrade.Name,
                    LowGradeId = LowGrade.GradeId,
                    HighGradeid = HighGrade.GradeId
                };
                return gr; 
            }
        }
        public bool? IsCareerTech { get; set; }
        public bool? IsSpecialEducation { get; set; }
        public bool? IsLocallyEditable { get; set; }
        public bool? IsCollege { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int? CourseLevelId { get; set; }
        public CourseLevel CourseLevel { get; set; }
        public int? GradeScaleId { get; set; }
        public GradeScale GradeScale { get; set; }
        public int? ScedCategoryId { get; set; }
        public ScedCategory ScedCategory { get; set; }

        public string Status { get; set; }
        public List<DraftDeliveryTypeDto> DeliveryTypes { get; set; }
        public List<DraftEndorsementDto> Endorsements { get; set; }

        public List<ProgramDraftDto> Programs { get; set; }

    }

    public class GradeRange
    {
        public int LowGradeId { get; set; }
        public string LowGrade { get; set; }
        public int HighGradeid { get; set; }
        public string HighGrade { get; set; }
    }

    public class ProgramDraftDto
    {
        public int ProgramDraftId { get; set; }
        public int ProgramId { get; set; }
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
        public int DraftDeliveryTypeId { get; set; }
        public int DeliveryTypeId { get; set; }
        public string DeliveryTypeName { get; set; }
    }
}