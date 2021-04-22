using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Refs.ProgramTypes.Commands.DeleteProgramType
{
    public class DeleteProgramTypeCommandHandler : IRequestHandler<DeleteProgramTypeCommand>
    {
        private readonly IProgramTypeRepository _programTypeRepository;

        public DeleteProgramTypeCommandHandler(IProgramTypeRepository programTypeRepository)
        {
            _programTypeRepository = programTypeRepository;
        }

        public async Task<Unit> Handle(DeleteProgramTypeCommand request, CancellationToken cancellationToken)
        {
            var programTypeToDelete = await _programTypeRepository.GetByIdAsync(request.ProgramTypeId);

            if (programTypeToDelete == null) throw new NotFoundException(nameof(Draft), request.ProgramTypeId);

            if (await _programTypeRepository.HasPrograms(request.ProgramTypeId))
                throw new BadRequestException("Program Type assigned to programs. Cannot delete.");

            await _programTypeRepository.DeleteAsync(programTypeToDelete);

            return Unit.Value;
        }
    }
}