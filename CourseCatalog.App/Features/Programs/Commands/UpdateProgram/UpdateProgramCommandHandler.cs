using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Programs.Commands.UpdateProgram
{
    public class UpdateProgramCommandHandler : IRequestHandler<UpdateProgramCommand>
    {
        private readonly IMapper _mapper;
        private readonly IProgramRepository _programRepository;

        public UpdateProgramCommandHandler(IMapper mapper, IProgramRepository programRepository)
        {
            _mapper = mapper;
            _programRepository = programRepository;
        }

        public async Task<Unit> Handle(UpdateProgramCommand request, CancellationToken cancellationToken)
        {
            var programToUpdate = await _programRepository.GetByIdAsync(request.ProgramId);

            if (programToUpdate == null) throw new NotFoundException(nameof(Program), request.ProgramId);

            var program = await _programRepository.GetProgramByProgramCode(request.ProgramCode);
            if (program != null && program.ProgramCode != programToUpdate.ProgramCode)
                throw new BadRequestException(
                    $"Duplicate Program Code. Existing program already contains Program Code {request.ProgramCode}");

            _mapper.Map(request, programToUpdate, typeof(UpdateProgramCommand), typeof(Program));

            await _programRepository.UpdateAsync(programToUpdate);

            return Unit.Value;
        }
    }
}