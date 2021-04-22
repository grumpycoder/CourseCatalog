using MediatR;

namespace CourseCatalog.App.Features.Programs.Queries.GetProgramSummary
{
    public class GetProgramSummaryQuery : IRequest<ProgramSummaryDto>
    {
    }
}