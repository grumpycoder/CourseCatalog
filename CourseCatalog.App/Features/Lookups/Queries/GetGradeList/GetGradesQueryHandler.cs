using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetGradeList
{
    public class GetGradeListQueryHandler : IRequestHandler<GetGradeListQuery, List<Grade>>
    {
        private readonly IAsyncRepository<Grade> _repository;

        public GetGradeListQueryHandler(IAsyncRepository<Grade> repository)
        {
            _repository = repository;
        }

        public async Task<List<Grade>> Handle(GetGradeListQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.ListAllAsync();
            return dto as List<Grade>;
        }
    }
}