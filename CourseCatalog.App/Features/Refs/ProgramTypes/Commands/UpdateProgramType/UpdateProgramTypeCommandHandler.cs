using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Refs.ProgramTypes.Commands.UpdateProgramType
{
    public class UpdateProgramTypeCommandHandler : IRequestHandler<UpdateProgramTypeCommand>
    {
        private readonly IMapper _mapper;
        private readonly IProgramTypeRepository _programTypeRepository;

        public UpdateProgramTypeCommandHandler(IMapper mapper, IProgramTypeRepository programTypeRepository)
        {
            _mapper = mapper;
            _programTypeRepository = programTypeRepository;
        }

        public async Task<Unit> Handle(UpdateProgramTypeCommand request, CancellationToken cancellationToken)
        {
            var programTypeToUpdate = await _programTypeRepository.GetByIdAsync(request.ProgramTypeId);
            if (programTypeToUpdate == null) throw new NotFoundException(nameof(Subject), request.ProgramTypeId);

            var programType = await _programTypeRepository.GetProgramTypeByName(request.Name);
            if (programType != null && programType.Name != programTypeToUpdate.Name)
                throw new BadRequestException(
                    $"Duplicate Name. Existing Program Type already contains name {request.Name}");

            _mapper.Map(request, programTypeToUpdate, typeof(UpdateProgramTypeCommand), typeof(ProgramType));

            await _programTypeRepository.UpdateAsync(programTypeToUpdate);

            return Unit.Value;
        }
    }
}