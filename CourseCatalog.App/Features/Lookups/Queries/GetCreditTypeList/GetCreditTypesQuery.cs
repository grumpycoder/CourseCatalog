using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetCreditTypeList
{
    public class GetCreditTypeListQuery : IRequest<List<Tag>>
    {
    }
}