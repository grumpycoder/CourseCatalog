using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Programs.Commands.DeleteProgramCredential
{
    public class DeleteProgramCredentialCommandHandler : IRequestHandler<DeleteProgramCredentialCommand>
    {
        private readonly IProgramRepository _programRepository;

        public DeleteProgramCredentialCommandHandler(IProgramRepository programRepository)
        {
            _programRepository = programRepository;
        }

        public async Task<Unit> Handle(DeleteProgramCredentialCommand request, CancellationToken cancellationToken)
        {
            var existingProgram = await _programRepository.GetProgramByIdWithDetails(request.ProgramId);

            if (existingProgram == null) throw new NotFoundException(nameof(Program), request.ProgramId);
            
            var credentialToDelete = existingProgram.Credentials.FirstOrDefault(e => e.CredentialId == request.CredentialId);
            if (credentialToDelete == null) throw new BadRequestException("Program does not contain credential");

            existingProgram.Credentials.Remove(credentialToDelete);

            await _programRepository.UpdateAsync(existingProgram);

            return Unit.Value;
        }
    }
}
