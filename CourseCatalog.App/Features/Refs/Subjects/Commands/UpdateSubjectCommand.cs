using MediatR;

namespace CourseCatalog.App.Features.Refs.Subjects.Commands
{
    public class UpdateSubjectCommand : IRequest
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string SubjectCode { get; set; }
    }
}
