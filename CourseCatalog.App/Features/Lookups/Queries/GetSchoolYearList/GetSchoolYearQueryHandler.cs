using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Lookups.Queries.GetSchoolYearList
{
    public class GetSchoolYearListQueryHandler : IRequestHandler<GetSchoolYearListQuery, List<SchoolYearDto>>
    {
        public GetSchoolYearListQueryHandler()
        {

        }

        public async Task<List<SchoolYearDto>> Handle(GetSchoolYearListQuery request, CancellationToken cancellationToken)
        {
            var currentYear = DateTime.Now.Year + 1;

            var years = Enumerable.Range(currentYear - 19, 20).Reverse().ToList();

            return years.Select(item => new SchoolYearDto() { Year = item }).ToList();
        }
    }
}
