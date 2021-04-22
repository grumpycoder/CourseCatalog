using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetScedCategoryList
{
    public class GetScedCategoryListQuery : IRequest<List<ScedCategory>>
    {
    }
}