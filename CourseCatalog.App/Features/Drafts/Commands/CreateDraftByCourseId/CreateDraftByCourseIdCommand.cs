using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.CreateDraftByCourseId
{
    public class CreateDraftByCourseIdCommand : IRequest<int>
    {
        public int CourseId { get; set; }

        public CreateDraftByCourseIdCommand(int courseId)
        {
            CourseId = courseId;
        }
    }

}
