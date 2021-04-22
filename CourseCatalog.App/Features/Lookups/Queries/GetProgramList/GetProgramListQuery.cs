using System.Collections.Generic;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetProgramList
{
    public class GetProgramListQuery : IRequest<List<ProgramListDto>>
    {
    }
}