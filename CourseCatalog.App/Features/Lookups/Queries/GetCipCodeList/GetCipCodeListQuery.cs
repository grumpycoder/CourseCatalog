using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Lookups.Queries.GetCipCodeList
{
    public class GetCipCodeListQuery : IRequest<List<Cip>>
    {

    }
}
