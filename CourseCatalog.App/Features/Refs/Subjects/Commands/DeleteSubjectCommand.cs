using MediatR;

namespace CourseCatalog.App.Features.Refs.Subjects.Commands
{
    public class DeleteSubjectCommand : IRequest
    {
        public int SubjectId { get; set; }

        public DeleteSubjectCommand(int subjectId)
        {
            SubjectId = subjectId;
        }
    }
}
