using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Refs.DeliveryTypes.Commands.CreateDeliveryType
{
    public class CreateDeliveryTypeCommandHandler : IRequestHandler<CreateDeliveryTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IDeliveryTypeRepository _deliveryTypeRepository;

        public CreateDeliveryTypeCommandHandler(IMapper mapper, IDeliveryTypeRepository deliveryTypeRepository)
        {
            _mapper = mapper;
            _deliveryTypeRepository = deliveryTypeRepository;
        }

        public async Task<int> Handle(CreateDeliveryTypeCommand request, CancellationToken cancellationToken)
        {
            var deliveryType = await _deliveryTypeRepository.GetDeliveryTypeByName(request.Name);
            if (deliveryType != null)
            {
                throw new BadRequestException(
                    $"Duplicate Delivery Type Name. Existing Delivery Type already contains name {request.Name}");
            }

            deliveryType = _mapper.Map<DeliveryType>(request);

            deliveryType = await _deliveryTypeRepository.AddAsync(deliveryType);

            return deliveryType.DeliveryTypeId;
        }
    }
}
