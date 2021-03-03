using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Programs.Commands.CreateProgramCredential
{
    public class CreateProgramCredentialCommandHandler : IRequestHandler<CreateProgramCredentialCommand, CreateProgramCredentialDto>
    {
        private readonly IMapper _mapper;
        private readonly IProgramRepository _programRepository;

        public CreateProgramCredentialCommandHandler(IMapper mapper, IProgramRepository programRepository)
        {
            _mapper = mapper;
            _programRepository = programRepository;
        }

        public async Task<CreateProgramCredentialDto> Handle(CreateProgramCredentialCommand request, CancellationToken cancellationToken)
        {
            var programToUpdate = await _programRepository.GetProgramByIdWithDetails(request.ProgramId);

            if (programToUpdate == null)
            {
                throw new NotFoundException(nameof(Program), request.ProgramId);
            }

            if (programToUpdate.Credentials.Any(c => c.CredentialId == request.CredentialId))
            {
                throw new BadRequestException(
                    "Credential already assigned to Program");
            }

            var programCredential = new ProgramCredential
            {
                BeginYear = request.BeginYear,
                EndYear = request.EndYear,
                CredentialId = request.CredentialId
            };

            programToUpdate.Credentials.Add(programCredential);

            await _programRepository.UpdateAsync(programToUpdate);

            programToUpdate = await _programRepository.GetProgramByIdWithDetails(request.ProgramId);

            programCredential =
                programToUpdate.Credentials.FirstOrDefault(c => c.ProgramCredentialId == programCredential.ProgramCredentialId);

            var dto = _mapper.Map<CreateProgramCredentialDto>(programCredential);

            return dto;

        }
    }
}
