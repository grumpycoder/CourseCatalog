using AutoMapper;
using CourseCatalog.App.Features.Refs.CourseLevels.Commands.UpdateCourseLevel;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Refs.DeliveryTypes.Commands.UpdateDeliveryType
{
    public class UpdateDeliveryTypeCommandHandler : IRequestHandler<UpdateDeliveryTypeCommand>
    {
        private readonly IMapper _mapper;
        private readonly IDeliveryTypeRepository _deliveryTypeRepository;

        public UpdateDeliveryTypeCommandHandler(IMapper mapper, IDeliveryTypeRepository deliveryTypeRepository)
        {
            _mapper = mapper;
            _deliveryTypeRepository = deliveryTypeRepository;
        }

        public async Task<Unit> Handle(UpdateDeliveryTypeCommand request, CancellationToken cancellationToken)
        {
            var deliveryTypeToUpdate = await _deliveryTypeRepository.GetByIdAsync(request.DeliveryTypeId);
            if (deliveryTypeToUpdate == null) throw new NotFoundException(nameof(Subject), request.DeliveryTypeId);

            var deliveryType = await _deliveryTypeRepository.GetDeliveryTypeByName(request.Name);
            if (deliveryType != null && deliveryType.Name != deliveryTypeToUpdate.Name)
            {
                throw new BadRequestException(
                    $"Duplicate Name. Existing Delivery Type already contains name {request.Name}");
            }

            _mapper.Map(request, deliveryTypeToUpdate, typeof(UpdateDeliveryTypeCommand), typeof(DeliveryType));

            await _deliveryTypeRepository.UpdateAsync(deliveryTypeToUpdate);

            return Unit.Value;
        }
    }
}
