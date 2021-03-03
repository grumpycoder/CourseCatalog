using CourseCatalog.App.Features.Courses.Queries.GetCourseSummary;
using CourseCatalog.Application.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.App.Features.Programs.Queries.GetProgramSummary
{
    public class GetProgramSummaryQueryHandler : IRequestHandler<GetProgramSummaryQuery, ProgramSummaryDto>
    {
        private readonly IAsyncRepository<Program> _programRepository;
        private readonly IAsyncRepository<Credential> _credentialRepository;

        public GetProgramSummaryQueryHandler(IAsyncRepository<Program> programRepository, IAsyncRepository<Credential> credentialRepository)
        {
            _programRepository = programRepository;
            _credentialRepository = credentialRepository;
        }

        public async Task<ProgramSummaryDto> Handle(GetProgramSummaryQuery request, CancellationToken cancellationToken)
        {
            var programsCount = await _programRepository.Count();
            var credentialsCount = await _credentialRepository.Count();

            var dto = new ProgramSummaryDto()
            {
                ActiveProgramsCount = programsCount,
                ActiveCredentialsCount = credentialsCount
            };

            return dto;
        }
    }
}
