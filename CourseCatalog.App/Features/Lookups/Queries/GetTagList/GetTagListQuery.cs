using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetTagList
{
    public class GetTagListQuery : IRequest<List<Tag>>
    {
    }
}