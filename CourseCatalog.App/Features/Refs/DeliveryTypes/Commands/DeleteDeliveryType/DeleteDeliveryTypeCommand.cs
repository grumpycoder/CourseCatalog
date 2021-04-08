using MediatR;

namespace CourseCatalog.App.Features.Refs.DeliveryTypes.Commands.DeleteDeliveryType
{
    public class DeleteDeliveryTypeCommand : IRequest
    {
        public int DeliveryTypeId { get; set; }

        public DeleteDeliveryTypeCommand(int deliveryTypeId)
        {
            DeliveryTypeId = deliveryTypeId;
        }
    }
}
