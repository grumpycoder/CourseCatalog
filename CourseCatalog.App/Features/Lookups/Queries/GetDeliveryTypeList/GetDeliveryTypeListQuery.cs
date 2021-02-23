using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Lookups.Queries.GetDeliveryTypeList
{
    public class GetDeliveryTypeListQuery : IRequest<List<DeliveryType>>
    {

    }
}
