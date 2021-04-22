using MediatR;

namespace CourseCatalog.App.Features.Refs.Subjects.Commands
{
    public class DeleteSubjectCommand : IRequest
    {
        public DeleteSubjectCommand(int subjectId)
        {
            SubjectId = subjectId;
        }

        public int SubjectId { get; set; }
    }
}