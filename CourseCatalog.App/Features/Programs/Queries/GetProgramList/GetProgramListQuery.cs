using System.Collections.Generic;
using MediatR;

namespace CourseCatalog.App.Features.Programs.Queries.GetProgramList
{
    public class GetProgramListQuery : IRequest<List<ProgramListDto>>
    {
    }
}