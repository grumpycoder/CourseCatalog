using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Credentials.Commands.DeleteCredentialProgram
{
    public class DeleteCredentialProgramCommandHandler : IRequestHandler<DeleteCredentialProgramCommand>
    {
        private readonly ICredentialRepository _credentialRepository;

        public DeleteCredentialProgramCommandHandler(ICredentialRepository credentialRepository)
        {
            _credentialRepository = credentialRepository;
        }

        public async Task<Unit> Handle(DeleteCredentialProgramCommand request, CancellationToken cancellationToken)
        {
            var existingCredential = await _credentialRepository.GetCredentialByIdWithDetails(request.CredentialId);

            if (existingCredential == null) throw new NotFoundException(nameof(Credential), request.CredentialId);

            var programToDelete = existingCredential.Programs.FirstOrDefault(e => e.ProgramId == request.ProgramId);
            if (programToDelete == null) throw new BadRequestException("Credential does not contain Program");

            existingCredential.Programs.Remove(programToDelete);

            await _credentialRepository.UpdateAsync(existingCredential);

            return Unit.Value;
        }
    }
}
