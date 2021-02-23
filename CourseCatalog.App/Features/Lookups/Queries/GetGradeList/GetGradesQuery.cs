using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Lookups.Queries.GetGradeList
{
    public class GetGradeListQuery : IRequest<List<Grade>>
    {

    }
}
