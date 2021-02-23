using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Lookups.Queries.GetCourseLevelList
{
    public class GetCourseLevelListQueryHandler : IRequestHandler<GetCourseLevelListQuery, List<CourseLevel>>
    {
        private readonly IAsyncRepository<CourseLevel> _repository;

        public GetCourseLevelListQueryHandler(IAsyncRepository<CourseLevel> repository)
        {
            _repository = repository;
        }

        public async Task<List<CourseLevel>> Handle(GetCourseLevelListQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.ListAllAsync();
            return dto as List<CourseLevel>;
        }
    }
}
