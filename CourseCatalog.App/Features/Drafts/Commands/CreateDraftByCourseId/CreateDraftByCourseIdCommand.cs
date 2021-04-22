using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.CreateDraftByCourseId
{
    public class CreateDraftByCourseIdCommand : IRequest<int>
    {
        public CreateDraftByCourseIdCommand(int courseId)
        {
            CourseId = courseId;
        }

        public int CourseId { get; set; }
    }
}