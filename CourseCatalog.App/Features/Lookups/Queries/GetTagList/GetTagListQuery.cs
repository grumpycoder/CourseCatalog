using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Lookups.Queries.GetTagList
{
    public class GetTagListQuery : IRequest<List<Tag>>
    {

    }
}
