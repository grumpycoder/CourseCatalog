using MediatR;

namespace CourseCatalog.App.Features.Refs.CreditTypes.Commands.DeleteCreditType
{
    public class DeleteCreditTypeCommand : IRequest
    {
        public DeleteCreditTypeCommand(int tagId)
        {
            TagId = tagId;
        }

        public int TagId { get; set; }
    }
}