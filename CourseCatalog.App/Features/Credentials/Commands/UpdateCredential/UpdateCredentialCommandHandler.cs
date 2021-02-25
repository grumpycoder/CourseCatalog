using AutoMapper;
using MediatR;
using MyDemo.Api.Application.Contracts;
using MyDemo.Api.Domain.Entities;
using MyDemo.Api.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace MyDemo.Api.Features.Credentials.Commands.UpdateCredential
{
    public class UpdateCredentialCommandHandler : IRequestHandler<UpdateCredentialCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICredentialRepository _credentialRepository;

        public UpdateCredentialCommandHandler(IMapper mapper, ICredentialRepository credentialRepository)
        {
            _mapper = mapper;
            _credentialRepository = credentialRepository;
        }

        public async Task<Unit> Handle(UpdateCredentialCommand request, CancellationToken cancellationToken)
        {
            var credentialToUpdate = await _credentialRepository.GetByIdAsync(request.CredentialId);

            if (credentialToUpdate == null)
            {
                throw new NotFoundException(nameof(Credential), request.CredentialId);
            }

            var credential = await _credentialRepository.GetCredentialByCredentialCode(request.CredentialCode);
            if (credential != null && credential.CredentialCode != credentialToUpdate.CredentialCode)
            {
                throw new BadRequestException(
                    $"Duplicate Credential Code. Existing Credential already contains Credential Code {request.CredentialCode}");
            }

            _mapper.Map(request, credentialToUpdate, typeof(UpdateCredentialCommand), typeof(Credential));

            await _credentialRepository.UpdateAsync(credentialToUpdate);

            return Unit.Value;

        }
    }
}
