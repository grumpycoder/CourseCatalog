using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Refs.CredentialTypes.Commands.UpdateCredentialType
{
    public class UpdateCredentialTypeCommandHandler : IRequestHandler<UpdateCredentialTypeCommand>
    {
        private readonly ICredentialTypeRepository _credentialTypeRepository;
        private readonly IMapper _mapper;

        public UpdateCredentialTypeCommandHandler(IMapper mapper, ICredentialTypeRepository credentialTypeRepository)
        {
            _mapper = mapper;
            _credentialTypeRepository = credentialTypeRepository;
        }

        public async Task<Unit> Handle(UpdateCredentialTypeCommand request, CancellationToken cancellationToken)
        {
            var credentialTypeToUpdate = await _credentialTypeRepository.GetByIdAsync(request.CredentialTypeId);
            if (credentialTypeToUpdate == null) throw new NotFoundException(nameof(Subject), request.CredentialTypeId);

            var credentialType = await _credentialTypeRepository.GetCredentialTypeByName(request.Name);
            if (credentialType != null && credentialType.Name != credentialTypeToUpdate.Name)
                throw new BadRequestException(
                    $"Duplicate Name. Existing Credential Type already contains name {request.Name}");

            _mapper.Map(request, credentialTypeToUpdate, typeof(UpdateCredentialTypeCommand), typeof(CredentialType));

            await _credentialTypeRepository.UpdateAsync(credentialTypeToUpdate);

            return Unit.Value;
        }
    }
}