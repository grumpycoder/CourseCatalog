using MediatR;

namespace CourseCatalog.App.Features.Refs.DeliveryTypes.Commands.CreateDeliveryType
{
    public class CreateDeliveryTypeCommand : IRequest<int>
    {
        public int DeliveryTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}