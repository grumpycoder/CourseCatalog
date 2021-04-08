using MediatR;

namespace CourseCatalog.App.Features.Refs.DeliveryTypes.Commands.UpdateDeliveryType
{
    public class UpdateDeliveryTypeCommand : IRequest
    {
        public int DeliveryTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
