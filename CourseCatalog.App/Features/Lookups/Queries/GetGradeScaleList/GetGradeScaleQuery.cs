using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Lookups.Queries.GetGradeScaleList
{
    public class GetGradeScaleListQuery : IRequest<List<GradeScale>>
    {

    }
}
