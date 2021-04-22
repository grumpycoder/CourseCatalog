using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Clusters.Commands.UpdateCluster
{
    public class UpdateClusterCommandHandler : IRequestHandler<UpdateClusterCommand>
    {
        private readonly IClusterRepository _clusterRepository;
        private readonly IMapper _mapper;

        public UpdateClusterCommandHandler(IMapper mapper, IClusterRepository clusterRepository)
        {
            _mapper = mapper;
            _clusterRepository = clusterRepository;
        }

        public async Task<Unit> Handle(UpdateClusterCommand request, CancellationToken cancellationToken)
        {
            var clusterToUpdate = await _clusterRepository.GetByIdAsync(request.ClusterId);

            if (clusterToUpdate == null) throw new NotFoundException(nameof(Cluster), request.ClusterId);

            var cluster = await _clusterRepository.GetClusterByClusterCode(request.ClusterCode);
            if (cluster != null && cluster.ClusterCode != clusterToUpdate.ClusterCode)
                throw new BadRequestException(
                    $"Duplicate Cluster Code. Existing Cluster already contains Cluster Code {request.ClusterCode}");

            _mapper.Map(request, clusterToUpdate, typeof(UpdateClusterCommand), typeof(Cluster));

            await _clusterRepository.UpdateAsync(clusterToUpdate);

            return Unit.Value;
        }
    }
}