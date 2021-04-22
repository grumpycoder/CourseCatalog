using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetGradeList
{
    public class GetGradeListQuery : IRequest<List<Grade>>
    {
    }
}