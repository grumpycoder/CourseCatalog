using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetCipCodeList
{
    public class GetCipCodeListQuery : IRequest<List<Cip>>
    {
    }
}