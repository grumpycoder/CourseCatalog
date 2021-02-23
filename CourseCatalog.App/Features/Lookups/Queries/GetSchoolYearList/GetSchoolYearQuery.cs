using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Lookups.Queries.GetSchoolYearList
{
    public class GetSchoolYearListQuery : IRequest<List<SchoolYearDto>>
    {

    }
}
