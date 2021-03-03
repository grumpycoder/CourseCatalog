using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Drafts.Commands.UpdateDraft
{
    public class UpdateDraftCommand : IRequest
    {
        public int DraftId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CourseNumber { get; set; }
        public string CollegeCourseId { get; set; }
        public string CipCode { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
        public decimal? CreditHours { get; set; }
        public List<string> CreditTypes { get; set; }
        public List<string> Tags { get; set; }
        public int? LowGradeId { get; set; }
        public int? HighGradeId { get; set; }
        public bool? IsCareerTech { get; set; }
        public bool? IsSpecialEducation { get; set; }
        public bool? IsLocallyEditable { get; set; }
        public bool? IsCollege { get; set; }
        public int? SubjectId { get; set; }
        public int? CourseLevelId { get; set; }
        public int? GradeScaleId { get; set; }
        public int? ScedCategoryId { get; set; }

        public List<UpdateDraftDeliveryTypeDto> DeliveryTypes { get; set; }
    }

    public class UpdateDraftDeliveryTypeDto
    {
        public int DraftDeliveryTypeId { get; set; }
        public int DeliveryTypeId { get; set; }
        public int DraftId { get; set; }
    }

}
