using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Lookups.Queries.GetGradeScaleList
{
    public class GetGradeScaleListQueryHandler : IRequestHandler<GetGradeScaleListQuery, List<GradeScale>>
    {
        private readonly IAsyncRepository<GradeScale> _repository;

        public GetGradeScaleListQueryHandler(IAsyncRepository<GradeScale> repository)
        {
            _repository = repository;
        }

        public async Task<List<GradeScale>> Handle(GetGradeScaleListQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.ListAllAsync();
            return dto as List<GradeScale>;
        }
    }
}
