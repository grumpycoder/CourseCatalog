using MediatR;

namespace CourseCatalog.App.Features.Refs.CreditTypes.Commands.DeleteCreditType
{
    public class DeleteCreditTypeCommand : IRequest
    {
        public int TagId { get; set; }

        public DeleteCreditTypeCommand(int tagId)
        {
            TagId = tagId;
        }
    }
}
