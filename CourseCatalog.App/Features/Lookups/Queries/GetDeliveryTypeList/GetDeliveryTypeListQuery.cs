using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetDeliveryTypeList
{
    public class GetDeliveryTypeListQuery : IRequest<List<DeliveryType>>
    {
    }
}