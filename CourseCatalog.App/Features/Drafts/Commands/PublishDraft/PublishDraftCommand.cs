using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.PublishDraft
{
    public class PublishDraftCommand : IRequest
    {
        public int DraftId { get; set; }

        public PublishDraftCommand(int draftId)
        {
            DraftId = draftId;
        }
    }
}