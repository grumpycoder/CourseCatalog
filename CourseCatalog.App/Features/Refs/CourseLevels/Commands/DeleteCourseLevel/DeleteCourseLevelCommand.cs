using MediatR;

namespace CourseCatalog.App.Features.Refs.CourseLevels.Commands.DeleteCourseLevel
{
    public class DeleteCourseLevelCommand : IRequest
    {
        public DeleteCourseLevelCommand(int courseLevelId)
        {
            CourseLevelId = courseLevelId;
        }

        public int CourseLevelId { get; set; }
    }
}