using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Refs.ClusterTypes.Commands.UpdateClusterType
{
    public class UpdateClusterTypeCommandHandler : IRequestHandler<UpdateClusterTypeCommand>
    {
        private readonly IMapper _mapper;
        private readonly IClusterTypeRepository _clusterTypeRepository;

        public UpdateClusterTypeCommandHandler(IMapper mapper, IClusterTypeRepository clusterTypeRepository)
        {
            _mapper = mapper;
            _clusterTypeRepository = clusterTypeRepository;
        }

        public async Task<Unit> Handle(UpdateClusterTypeCommand request, CancellationToken cancellationToken)
        {
            var clusterTypeToUpdate = await _clusterTypeRepository.GetByIdAsync(request.ClusterTypeId);
            if (clusterTypeToUpdate == null) throw new NotFoundException(nameof(Subject), request.ClusterTypeId);

            var clusterType = await _clusterTypeRepository.GetClusterTypeByName(request.Name);
            if (clusterType != null && clusterType.Name != clusterTypeToUpdate.Name)
            {
                throw new BadRequestException(
                    $"Duplicate Name. Existing Cluster Type already contains name {request.Name}");
            }

            _mapper.Map(request, clusterTypeToUpdate, typeof(UpdateClusterTypeCommand), typeof(ClusterType));

            await _clusterTypeRepository.UpdateAsync(clusterTypeToUpdate);

            return Unit.Value;
        }
    }
}
