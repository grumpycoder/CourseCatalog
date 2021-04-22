using MediatR;

namespace CourseCatalog.App.Features.Refs.CreditTypes.Commands.UpdateCreditType
{
    public class UpdateCreditTypeCommand : IRequest
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}