using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace CourseCatalog.App.Features.Clusters.Commands.CreateCluster
{
    public class CreateClusterCommandHandler : IRequestHandler<CreateClusterCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IClusterRepository _clusterRepository;

        public CreateClusterCommandHandler(IMapper mapper, IClusterRepository clusterRepository)
        {
            _mapper = mapper;
            _clusterRepository = clusterRepository;
        }
        public async Task<int> Handle(CreateClusterCommand request, CancellationToken cancellationToken)
        {
            var cluster = await _clusterRepository.GetClusterByClusterCode(request.ClusterCode);
            if (cluster != null)
            {
                throw new BadRequestException(
                    $"Duplicate Cluster Code. Existing cluster already contains Cluster Code {request.ClusterCode}");
            }
            
            cluster = _mapper.Map<Cluster>(request);

            cluster = await _clusterRepository.AddAsync(cluster);

            return cluster.ClusterId;
        }
    }
}
