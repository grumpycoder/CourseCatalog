using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Refs.CourseLevels.Commands.DeleteCourseLevel
{
    public class DeleteCourseLevelCommandHandler : IRequestHandler<DeleteCourseLevelCommand>
    {
        private readonly ICourseLevelRepository _courseLevelRepository;

        public DeleteCourseLevelCommandHandler(ICourseLevelRepository courseLevelRepository)
        {
            _courseLevelRepository = courseLevelRepository;
        }

        public async Task<Unit> Handle(DeleteCourseLevelCommand request, CancellationToken cancellationToken)
        {
            var subjectToDelete = await _courseLevelRepository.GetByIdAsync(request.CourseLevelId);

            if (subjectToDelete == null) throw new NotFoundException(nameof(Draft), request.CourseLevelId);

            if (await _courseLevelRepository.HasCourses(request.CourseLevelId))
            {
                throw new BadRequestException("Course Level assigned to courses. Cannot delete.");
            }

            await _courseLevelRepository.DeleteAsync(subjectToDelete);

            return Unit.Value;
        }
    }
}
