using MediatR;

namespace CourseCatalog.App.Features.Refs.CourseLevels.Commands.UpdateCourseLevel
{
    public class UpdateCourseLevelCommand : IRequest
    {
        public int CourseLevelId { get; set; }
        public string CourseLevelCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
