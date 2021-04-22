using MediatR;

namespace CourseCatalog.App.Features.Refs.CreditTypes.Commands.CreateCreditType
{
    public class CreateCreditTypeCommand : IRequest<int>
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}