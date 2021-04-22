using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetDeliveryTypeList
{
    public class GetDeliveryTypeListQueryHandler : IRequestHandler<GetDeliveryTypeListQuery, List<DeliveryType>>
    {
        private readonly IAsyncRepository<DeliveryType> _repository;

        public GetDeliveryTypeListQueryHandler(IAsyncRepository<DeliveryType> repository)
        {
            _repository = repository;
        }

        public async Task<List<DeliveryType>> Handle(GetDeliveryTypeListQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await _repository.ListAllAsync();
            return dto as List<DeliveryType>;
        }
    }
}