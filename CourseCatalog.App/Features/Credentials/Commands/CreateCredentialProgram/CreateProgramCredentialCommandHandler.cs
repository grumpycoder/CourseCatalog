using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Credentials.Commands.CreateCredentialProgram
{
    public class
        CreateCredentialProgramCommandHandler : IRequestHandler<CreateCredentialProgramCommand,
            CreateCredentialProgramDto>
    {
        private readonly ICredentialRepository _credentialRepository;
        private readonly IMapper _mapper;

        public CreateCredentialProgramCommandHandler(IMapper mapper, ICredentialRepository credentialRepository)
        {
            _mapper = mapper;
            _credentialRepository = credentialRepository;
        }

        public async Task<CreateCredentialProgramDto> Handle(CreateCredentialProgramCommand request,
            CancellationToken cancellationToken)
        {
            var credentialToUpdate = await _credentialRepository.GetCredentialByIdWithDetails(request.CredentialId);

            if (credentialToUpdate == null) throw new NotFoundException(nameof(Program), request.ProgramId);

            if (credentialToUpdate.Programs.Any(c => c.ProgramId == request.ProgramId))
                throw new BadRequestException(
                    "Program already assigned to Credential");

            var programCredential = new ProgramCredential
            {
                BeginYear = request.BeginYear,
                EndYear = request.EndYear,
                ProgramId = request.ProgramId
            };

            credentialToUpdate.Programs.Add(programCredential);

            await _credentialRepository.UpdateAsync(credentialToUpdate);

            credentialToUpdate = await _credentialRepository.GetCredentialByIdWithDetails(request.CredentialId);

            programCredential =
                credentialToUpdate.Programs.FirstOrDefault(c =>
                    c.ProgramCredentialId == programCredential.ProgramCredentialId);

            var dto = _mapper.Map<CreateCredentialProgramDto>(programCredential);

            return dto;
        }
    }
}