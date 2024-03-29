﻿using System.Collections.Generic;
using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.CreateDraft
{
    public class CreateDraftCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CourseNumber { get; set; }
        public string ScedCourseNumber { get; set; }
        public string StateAttribute1 { get; set; }
        public string StateAttribute2 { get; set; }
        public string CollegeCourseId { get; set; }
        public string CipCode { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
        public decimal CreditHours { get; set; } = 0.00m;
        public List<string> CreditTypes { get; set; }
        public List<string> Tags { get; set; }
        public int? LowGradeId { get; set; }
        public int? HighGradeId { get; set; }
        public bool IsCareerTech { get; set; } = false;
        public bool? IsSpecialEducation { get; set; } = false;
        public bool? IsLocallyEditable { get; set; } = false;
        public bool? IsCollege { get; set; } = false;
        public bool IsFitness { get; set; } = false; 
        public int? SubjectId { get; set; }
        public int? CourseLevelId { get; set; }
        public int? GradeScaleId { get; set; }
        public int? ScedCategoryId { get; set; }
        public List<CreateDraftDeliveryTypeDto> DeliveryTypes { get; set; }
    }


    public class CreateDraftDeliveryTypeDto
    {
        public int DraftDeliveryTypeId { get; set; }
        public int DeliveryTypeId { get; set; }
        public int DraftId { get; set; }
    }
}