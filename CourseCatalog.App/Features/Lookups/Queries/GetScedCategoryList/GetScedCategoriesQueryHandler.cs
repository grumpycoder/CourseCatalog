using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Lookups.Queries.GetScedCategoryList
{
    public class GetScedCategoryListQueryHandler : IRequestHandler<GetScedCategoryListQuery, List<ScedCategory>>
    {
        private readonly IAsyncRepository<ScedCategory> _repository;

        public GetScedCategoryListQueryHandler(IAsyncRepository<ScedCategory> repository)
        {
            _repository = repository;
        }

        public async Task<List<ScedCategory>> Handle(GetScedCategoryListQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.ListAllAsync();
            return dto as List<ScedCategory>;
        }
    }
}
