using System.Collections.Generic;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetSchoolYearList
{
    public class GetSchoolYearListQuery : IRequest<List<SchoolYearDto>>
    {
    }
}