using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Refs.CredentialTypes.Commands.CreateCredentialType
{
    public class CreateCredentialTypeCommandHandler : IRequestHandler<CreateCredentialTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ICredentialTypeRepository _clusterTypeRepository;

        public CreateCredentialTypeCommandHandler(IMapper mapper, ICredentialTypeRepository clusterTypeRepository)
        {
            _mapper = mapper;
            _clusterTypeRepository = clusterTypeRepository;
        }

        public async Task<int> Handle(CreateCredentialTypeCommand request, CancellationToken cancellationToken)
        {
            var clusterType = await _clusterTypeRepository.GetCredentialTypeByName(request.Name);
            if (clusterType != null)
            {
                throw new BadRequestException(
                    $"Duplicate Credential Type Name. Existing Credential Type already contains name {request.Name}");
            }

            clusterType = _mapper.Map<CredentialType>(request);

            clusterType = await _clusterTypeRepository.AddAsync(clusterType);

            return clusterType.CredentialTypeId;
        }
    }
}
