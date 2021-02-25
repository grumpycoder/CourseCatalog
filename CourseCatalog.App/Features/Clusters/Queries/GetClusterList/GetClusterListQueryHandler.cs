using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using MediatR;

namespace CourseCatalog.App.Features.Clusters.Queries.GetClusterList
{
    public class GetClusterListQueryHandler : IRequestHandler<GetClusterListQuery, List<ClusterListDto>>
    {
        private readonly IMapper _mapper;
        private readonly IClusterRepository _clusterRepository;

        public GetClusterListQueryHandler(IMapper mapper, IClusterRepository clusterRepository)
        {
            _mapper = mapper;
            _clusterRepository = clusterRepository;
        }

        public async Task<List<ClusterListDto>> Handle(GetClusterListQuery request, CancellationToken cancellationToken)
        {
            var clusters = await _clusterRepository.GetClustersWithDetails();
            return _mapper.Map<List<ClusterListDto>>(clusters);
        }
    }
}
