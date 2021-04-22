using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Clusters.Commands.CreateCluster
{
    public class CreateClusterCommandHandler : IRequestHandler<CreateClusterCommand, int>
    {
        private readonly IClusterRepository _clusterRepository;
        private readonly IMapper _mapper;

        public CreateClusterCommandHandler(IMapper mapper, IClusterRepository clusterRepository)
        {
            _mapper = mapper;
            _clusterRepository = clusterRepository;
        }

        public async Task<int> Handle(CreateClusterCommand request, CancellationToken cancellationToken)
        {
            var cluster = await _clusterRepository.GetClusterByClusterCode(request.ClusterCode);
            if (cluster != null)
                throw new BadRequestException(
                    $"Duplicate Cluster Code. Existing cluster already contains Cluster Code {request.ClusterCode}");

            cluster = _mapper.Map<Cluster>(request);

            cluster = await _clusterRepository.AddAsync(cluster);

            return cluster.ClusterId;
        }
    }
}