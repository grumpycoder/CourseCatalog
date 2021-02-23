using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Lookups.Queries.GetProgramList
{
    public class GetProgramListQuery : IRequest<List<Program>>
    {
        //TODO: Create ProgramDto
    }
}
