using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.DeleteDraft
{
    public class DeleteDraftCommand : IRequest
    {
        public int DraftId { get; set; }

        public DeleteDraftCommand(int draftId)
        {
            DraftId = draftId;
        }
    }


}
