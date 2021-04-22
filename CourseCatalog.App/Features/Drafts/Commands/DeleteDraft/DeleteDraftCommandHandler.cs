using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.DeleteDraft
{
    public class DeleteDraftCommandHandler : IRequestHandler<DeleteDraftCommand>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IDraftRepository _draftRepository;

        public DeleteDraftCommandHandler(IDraftRepository draftRepository, ICourseRepository courseRepository)
        {
            _draftRepository = draftRepository;
            _courseRepository = courseRepository;
        }

        public async Task<Unit> Handle(DeleteDraftCommand request, CancellationToken cancellationToken)
        {
            var draftToDelete = await _draftRepository.GetDraftByIdWithDetails(request.DraftId);

            if (draftToDelete == null) throw new NotFoundException(nameof(Draft), request.DraftId);

            if (draftToDelete.Status == CourseStatus.ExistingCourse)
            {
                var courseToUpdate = await _courseRepository.GetCourseByCourseNumber(draftToDelete.CourseNumber);
                courseToUpdate.Status = CourseStatus.Published;
                await _courseRepository.UpdateAsync(courseToUpdate);
            }

            await _draftRepository.DeleteAsync(draftToDelete);

            return Unit.Value;
        }
    }
}