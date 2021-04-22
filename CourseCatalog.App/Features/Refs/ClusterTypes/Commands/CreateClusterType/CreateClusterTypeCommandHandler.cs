using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Refs.ClusterTypes.Commands.CreateClusterType
{
    public class CreateClusterTypeCommandHandler : IRequestHandler<CreateClusterTypeCommand, int>
    {
        private readonly IClusterTypeRepository _clusterTypeRepository;
        private readonly IMapper _mapper;

        public CreateClusterTypeCommandHandler(IMapper mapper, IClusterTypeRepository clusterTypeRepository)
        {
            _mapper = mapper;
            _clusterTypeRepository = clusterTypeRepository;
        }

        public async Task<int> Handle(CreateClusterTypeCommand request, CancellationToken cancellationToken)
        {
            var clusterType = await _clusterTypeRepository.GetClusterTypeByName(request.Name);
            if (clusterType != null)
                throw new BadRequestException(
                    $"Duplicate Cluster Type Name. Existing Cluster Type already contains name {request.Name}");

            clusterType = _mapper.Map<ClusterType>(request);

            clusterType = await _clusterTypeRepository.AddAsync(clusterType);

            return clusterType.ClusterTypeId;
        }
    }
}