using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Refs.CredentialTypes.Commands.DeleteCredentialType
{
    public class DeleteCredentialTypeCommandHandler : IRequestHandler<DeleteCredentialTypeCommand>
    {
        private readonly ICredentialTypeRepository _programTypeRepository;

        public DeleteCredentialTypeCommandHandler(ICredentialTypeRepository programTypeRepository)
        {
            _programTypeRepository = programTypeRepository;
        }

        public async Task<Unit> Handle(DeleteCredentialTypeCommand request, CancellationToken cancellationToken)
        {
            var programTypeToDelete = await _programTypeRepository.GetByIdAsync(request.CredentialTypeId);

            if (programTypeToDelete == null) throw new NotFoundException(nameof(Draft), request.CredentialTypeId);

            if (await _programTypeRepository.HasCredentials(request.CredentialTypeId))
            {
                throw new BadRequestException("Credential Type assigned to Credentials. Cannot delete.");
            }

            await _programTypeRepository.DeleteAsync(programTypeToDelete);

            return Unit.Value;
        }
    }
}
