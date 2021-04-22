using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Clusters.Queries.GetClusterDetail
{
    public class GetClusterDetailQueryHandler : IRequestHandler<GetClusterDetailQuery, ClusterDetailDto>
    {
        private readonly IClusterRepository _clusterRepository;
        private readonly IMapper _mapper;

        public GetClusterDetailQueryHandler(IMapper mapper, IClusterRepository clusterRepository
        )
        {
            _mapper = mapper;
            _clusterRepository = clusterRepository;
        }

        public async Task<ClusterDetailDto> Handle(GetClusterDetailQuery request, CancellationToken cancellationToken)
        {
            var cluster = await _clusterRepository.GetClusterWithDetails(request.ClusterId);

            if (cluster == null) throw new NotFoundException(nameof(Cluster), request.ClusterId);
            var courseDetailDto = _mapper.Map<ClusterDetailDto>(cluster);

            return courseDetailDto;
        }
    }
}