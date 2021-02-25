using AutoMapper;
using MediatR;
using MyDemo.Api.Application.Contracts;
using MyDemo.Api.Domain.Entities;
using MyDemo.Api.Exceptions;
using MyDemo.Api.Features.Clusters.Queries.GetClusterDetail;
using System.Threading;
using System.Threading.Tasks;

namespace MyDemo.Api.Features.Programs.Queries.GetProgramDetail
{
    public class GetProgramDetailQueryHandler : IRequestHandler<GetProgramDetailQuery, ProgramDetailVm>
    {
        private readonly IProgramRepository _programRepository;
        private readonly IMapper _mapper;

        public GetProgramDetailQueryHandler(IMapper mapper, IProgramRepository programRepository
            )
        {
            _mapper = mapper;
            _programRepository = programRepository;
        }

        public async Task<ProgramDetailVm> Handle(GetProgramDetailQuery request, CancellationToken cancellationToken)
        {
            var program = await _programRepository.GetProgramWithDetails(request.ProgramId);

            if (program == null)
            {
                throw new NotFoundException(nameof(Cluster), request.ProgramId);
            }
            var programDetailDto = _mapper.Map<ProgramDetailVm>(program);

            return programDetailDto;
        }
    }
}
