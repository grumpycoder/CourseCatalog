using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetProgramTypeList
{
    public class GetProgramTypeListQuery : IRequest<List<ProgramType>>
    {
    }
}