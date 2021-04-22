using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Programs.Queries.GetProgramSummary
{
    public class GetProgramSummaryQueryHandler : IRequestHandler<GetProgramSummaryQuery, ProgramSummaryDto>
    {
        private readonly IAsyncRepository<Credential> _credentialRepository;
        private readonly IAsyncRepository<Program> _programRepository;

        public GetProgramSummaryQueryHandler(IAsyncRepository<Program> programRepository,
            IAsyncRepository<Credential> credentialRepository)
        {
            _programRepository = programRepository;
            _credentialRepository = credentialRepository;
        }

        public async Task<ProgramSummaryDto> Handle(GetProgramSummaryQuery request, CancellationToken cancellationToken)
        {
            var programsCount = await _programRepository.Count();
            var credentialsCount = await _credentialRepository.Count();

            var dto = new ProgramSummaryDto
            {
                ActiveProgramsCount = programsCount,
                ActiveCredentialsCount = credentialsCount
            };

            return dto;
        }
    }
}