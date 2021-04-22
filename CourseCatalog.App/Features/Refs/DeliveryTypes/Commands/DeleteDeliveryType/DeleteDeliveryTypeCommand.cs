using MediatR;

namespace CourseCatalog.App.Features.Refs.DeliveryTypes.Commands.DeleteDeliveryType
{
    public class DeleteDeliveryTypeCommand : IRequest
    {
        public DeleteDeliveryTypeCommand(int deliveryTypeId)
        {
            DeliveryTypeId = deliveryTypeId;
        }

        public int DeliveryTypeId { get; set; }
    }
}