using CourseCatalog.Domain.Entities;
using System;
using System.Collections.Generic;

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
        public List<string> CreditTypes { get; set; }
        public string Tags { get; set; }

        public Grade LowGrade { get; set; }
        public Grade HighGrade { get; set; }

        public GradeRange GradeRange
        {
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
        public DateTime? PublishDate { get; set; }

        public Subject Subject { get; set; }

        public CourseLevel CourseLevel { get; set; }
        public GradeScale GradeScale { get; set; }
        public ScedCategory ScedCategory { get; set; }

        public List<CourseDeliveryTypeDto> DeliveryTypes { get; set; }
        public List<CourseEndorsementDto> Endorsements { get; set; }

        public List<ProgramCourseDto> Programs { get; set; }

    }

    public class GradeRange
    {
        public int LowGradeId { get; set; }
        public string LowGrade { get; set; }
        public int HighGradeid { get; set; }
        public string HighGrade { get; set; }
    }

    public class ProgramCourseDto
    {
        public int ProgramCourseId { get; set; }
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
    }

    public class CourseEndorsementDto
    {
        public int CourseEndorsementId { get; set; }
        public int EndorsementId { get; set; }
        public string EndorseCode { get; set; }
        public string Description { get; set; }
    }

    public class CourseDeliveryTypeDto
    {
        public int CourseDeliveryTypeId { get; set; }
        public int DeliveryTypeId { get; set; }
        public string DeliveryTypeName { get; set; }
    }
}
