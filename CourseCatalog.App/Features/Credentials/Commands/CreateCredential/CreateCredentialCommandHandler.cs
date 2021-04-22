using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Credentials.Commands.CreateCredential
{
    public class CreateCredentialCommandHandler : IRequestHandler<CreateCredentialCommand, int>
    {
        private readonly ICredentialRepository _credentialRepository;
        private readonly IMapper _mapper;

        public CreateCredentialCommandHandler(IMapper mapper, ICredentialRepository credentialRepository)
        {
            _mapper = mapper;
            _credentialRepository = credentialRepository;
        }

        public async Task<int> Handle(CreateCredentialCommand request, CancellationToken cancellationToken)
        {
            var credentialToAdd = await _credentialRepository.GetCredentialByCredentialCode(request.CredentialCode);
            if (credentialToAdd != null)
                throw new BadRequestException(
                    $"Duplicate Credential Code. Existing Credential already contains Credential Code {request.CredentialCode}");

            var credential = await _credentialRepository.GetCredentialByCredentialCode(request.CredentialCode);
            if (credential != null)
                throw new BadRequestException(
                    $"Duplicate Credential Code. Existing Credential already contains Credential Code {request.CredentialCode}");

            credentialToAdd = _mapper.Map<Credential>(request);

            credentialToAdd = await _credentialRepository.AddAsync(credentialToAdd);

            return credentialToAdd.CredentialId;
        }
    }
}