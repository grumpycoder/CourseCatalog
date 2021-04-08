using MediatR;

namespace CourseCatalog.App.Features.Refs.CourseLevels.Commands.CreateCourseLevel
{
    public class CreateCourseLevelCommand : IRequest<int>
    {
        public int CourseLevelId { get; set; }
        public string CourseLevelCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
