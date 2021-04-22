using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetGradeScaleList
{
    public class GetGradeScaleListQuery : IRequest<List<GradeScale>>
    {
    }
}