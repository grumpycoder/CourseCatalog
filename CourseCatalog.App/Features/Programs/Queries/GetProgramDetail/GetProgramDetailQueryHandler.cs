using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Programs.Queries.GetProgramDetail
{
    public class GetProgramDetailQueryHandler : IRequestHandler<GetProgramDetailQuery, ProgramDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly IProgramRepository _programRepository;

        public GetProgramDetailQueryHandler(IMapper mapper, IProgramRepository programRepository
        )
        {
            _mapper = mapper;
            _programRepository = programRepository;
        }

        public async Task<ProgramDetailDto> Handle(GetProgramDetailQuery request, CancellationToken cancellationToken)
        {
            var program = await _programRepository.GetProgramByIdWithDetails(request.ProgramId);

            if (program == null) throw new NotFoundException(nameof(Program), request.ProgramId);

            var programDetailDto = _mapper.Map<ProgramDetailDto>(program);

            return programDetailDto;
        }
    }
}