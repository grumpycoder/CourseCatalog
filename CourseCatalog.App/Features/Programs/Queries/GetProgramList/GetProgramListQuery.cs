using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Programs.Queries.GetProgramList
{
    public class GetProgramListQuery : IRequest<List<ProgramListDto>>
    {

    }
}
