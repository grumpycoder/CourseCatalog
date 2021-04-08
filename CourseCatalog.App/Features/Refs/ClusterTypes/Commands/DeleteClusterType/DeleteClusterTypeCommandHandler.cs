using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Refs.ClusterTypes.Commands.DeleteClusterType
{
    public class DeleteClusterTypeCommandHandler : IRequestHandler<DeleteClusterTypeCommand>
    {
        private readonly IClusterTypeRepository _clusterTypeRepository;

        public DeleteClusterTypeCommandHandler(IClusterTypeRepository clusterTypeRepository)
        {
            _clusterTypeRepository = clusterTypeRepository;
        }

        public async Task<Unit> Handle(DeleteClusterTypeCommand request, CancellationToken cancellationToken)
        {
            var clusterTypeToDelete = await _clusterTypeRepository.GetByIdAsync(request.ClusterTypeId);

            if (clusterTypeToDelete == null) throw new NotFoundException(nameof(Draft), request.ClusterTypeId);

            if (await _clusterTypeRepository.HasClusters(request.ClusterTypeId))
            {
                throw new BadRequestException("Cluster Type assigned to clusters. Cannot delete.");
            }

            await _clusterTypeRepository.DeleteAsync(clusterTypeToDelete);

            return Unit.Value;
        }
    }
}
