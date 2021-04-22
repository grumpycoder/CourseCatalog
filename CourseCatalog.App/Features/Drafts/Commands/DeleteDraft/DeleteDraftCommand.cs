using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.DeleteDraft
{
    public class DeleteDraftCommand : IRequest
    {
        public DeleteDraftCommand(int draftId)
        {
            DraftId = draftId;
        }

        public int DraftId { get; set; }
    }
}