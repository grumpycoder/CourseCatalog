using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using MediatR;

namespace CourseCatalog.App.Features.Courses.Queries.GetCourseSummary
{
    public class GetCourseSummaryQueryHandler : IRequestHandler<GetCourseSummaryQuery, CourseSummaryDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IDraftRepository _draftRepository;

        public GetCourseSummaryQueryHandler(ICourseRepository courseRepository, IDraftRepository draftRepository)
        {
            _courseRepository = courseRepository;
            _draftRepository = draftRepository;
        }

        public async Task<CourseSummaryDto> Handle(GetCourseSummaryQuery request, CancellationToken cancellationToken)
        {
            var activeCourseCount = await _courseRepository.GetActiveCourseCount();
            var draftCount = await _draftRepository.GetDraftCount();

            var dto = new CourseSummaryDto
            {
                ActiveCourseCount = activeCourseCount,
                DraftCount = draftCount
            };

            return dto;
        }
    }
}