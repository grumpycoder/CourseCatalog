using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Lookups.Queries.GetScedCategoryList
{
    public class GetScedCategoryListQuery : IRequest<List<ScedCategory>>
    {

    }
}
