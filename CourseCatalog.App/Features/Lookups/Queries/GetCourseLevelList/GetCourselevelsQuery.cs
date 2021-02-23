using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetCourseLevelList
{
    public class GetCourseLevelListQuery : IRequest<List<CourseLevel>>
    {

    }
}
