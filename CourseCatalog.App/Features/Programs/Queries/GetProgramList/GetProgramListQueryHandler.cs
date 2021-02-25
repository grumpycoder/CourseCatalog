using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;

namespace CourseCatalog.App.Features.Programs.Queries.GetProgramList
{
    public class GetProgramListQueryHandler : IRequestHandler<GetProgramListQuery, List<ProgramListDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProgramRepository _programRepository;

        public GetProgramListQueryHandler(IMapper mapper, IProgramRepository programRepository)
        {
            _mapper = mapper;
            _programRepository = programRepository;
        }

        public async Task<List<ProgramListDto>> Handle(GetProgramListQuery request, CancellationToken cancellationToken)
        {
            var programs = await _programRepository.GetProgramsWithDetails();
            return _mapper.Map<List<ProgramListDto>>(programs);
        }
    }
}
