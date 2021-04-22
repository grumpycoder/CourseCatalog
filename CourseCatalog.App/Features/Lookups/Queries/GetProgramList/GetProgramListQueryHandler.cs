using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetProgramList
{
    public class GetProgramListQueryHandler : IRequestHandler<GetProgramListQuery, List<ProgramListDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProgramRepository _repository;

        public GetProgramListQueryHandler(IMapper mapper, IProgramRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<ProgramListDto>> Handle(GetProgramListQuery request, CancellationToken cancellationToken)
        {
            var programs = await _repository.GetProgramListWithDetails();
            return _mapper.Map<List<ProgramListDto>>(programs);
        }
    }
}