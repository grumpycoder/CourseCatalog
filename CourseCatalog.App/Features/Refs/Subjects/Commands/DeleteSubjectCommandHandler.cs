using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Refs.Subjects.Commands
{
    public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand>
    {
        private readonly ISubjectRepository _subjectRepository;

        public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Unit> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var subjectToDelete = await _subjectRepository.GetByIdAsync(request.SubjectId);

            if (subjectToDelete == null) throw new NotFoundException(nameof(Draft), request.SubjectId);

            if (await _subjectRepository.HasCourses(request.SubjectId))
                throw new BadRequestException("Subject assigned to courses. Cannot delete.");

            await _subjectRepository.DeleteAsync(subjectToDelete);

            return Unit.Value;
        }
    }
}