using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.PublishDraft
{
    public class PublishDraftCommand : IRequest
    {
        public PublishDraftCommand(int draftId)
        {
            DraftId = draftId;
        }

        public int DraftId { get; set; }
    }
}