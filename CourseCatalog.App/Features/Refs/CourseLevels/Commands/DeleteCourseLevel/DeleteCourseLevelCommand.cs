using MediatR;

namespace CourseCatalog.App.Features.Refs.CourseLevels.Commands.DeleteCourseLevel
{
    public class DeleteCourseLevelCommand : IRequest
    {
        public int CourseLevelId { get; set; }

        public DeleteCourseLevelCommand(int courseLevelId)
        {
            CourseLevelId = courseLevelId;
        }
    }
}
