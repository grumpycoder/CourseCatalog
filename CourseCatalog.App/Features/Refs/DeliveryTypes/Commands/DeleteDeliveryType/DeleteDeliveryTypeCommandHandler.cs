using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Exceptions;
using CourseCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Refs.DeliveryTypes.Commands.DeleteDeliveryType
{
    public class DeleteDeliveryTypeCommandHandler : IRequestHandler<DeleteDeliveryTypeCommand>
    {
        private readonly IDeliveryTypeRepository _deliveryTypeRepository;

        public DeleteDeliveryTypeCommandHandler(IDeliveryTypeRepository deliveryTypeRepository)
        {
            _deliveryTypeRepository = deliveryTypeRepository;
        }

        public async Task<Unit> Handle(DeleteDeliveryTypeCommand request, CancellationToken cancellationToken)
        {
            var deliveryTypeToDelete = await _deliveryTypeRepository.GetByIdAsync(request.DeliveryTypeId);

            if (deliveryTypeToDelete == null) throw new NotFoundException(nameof(Draft), request.DeliveryTypeId);

            if (await _deliveryTypeRepository.HasCourses(request.DeliveryTypeId))
                throw new BadRequestException("Delivery Type assigned to courses. Cannot delete.");

            await _deliveryTypeRepository.DeleteAsync(deliveryTypeToDelete);

            return Unit.Value;
        }
    }
}