using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Lookups.Queries.GetSubjectList
{
    public class GetClusterTypeListQueryHandler : IRequestHandler<GetClusterTypeListQuery, List<ClusterType>>
    {
        private readonly IAsyncRepository<ClusterType> _repository;

        public GetClusterTypeListQueryHandler(IAsyncRepository<ClusterType> repository)
        {
            _repository = repository;
        }

        public async Task<List<ClusterType>> Handle(GetClusterTypeListQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.ListAllAsync();
            return dto as List<ClusterType>;
        }
    }
}
