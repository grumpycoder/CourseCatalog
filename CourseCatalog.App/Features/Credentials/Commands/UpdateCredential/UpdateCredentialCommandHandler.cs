using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Credentials.Commands.UpdateCredential
{
    public class UpdateCredentialCommandHandler : IRequestHandler<UpdateCredentialCommand>
    {
        private readonly ICredentialRepository _credentialRepository;
        private readonly IMapper _mapper;

        public UpdateCredentialCommandHandler(IMapper mapper, ICredentialRepository credentialRepository)
        {
            _mapper = mapper;
            _credentialRepository = credentialRepository;
        }

        public async Task<Unit> Handle(UpdateCredentialCommand request, CancellationToken cancellationToken)
        {
            var credentialToUpdate = await _credentialRepository.GetByIdAsync(request.CredentialId);

            if (credentialToUpdate == null) throw new NotFoundException(nameof(Credential), request.CredentialId);

            var credential = await _credentialRepository.GetCredentialByCredentialCode(request.CredentialCode);
            if (credential != null && credential.CredentialCode != credentialToUpdate.CredentialCode)
                throw new BadRequestException(
                    $"Duplicate Credential Code. Existing Credential already contains Credential Code {request.CredentialCode}");

            _mapper.Map(request, credentialToUpdate, typeof(UpdateCredentialCommand), typeof(Credential));

            await _credentialRepository.UpdateAsync(credentialToUpdate);

            return Unit.Value;
        }
    }
}