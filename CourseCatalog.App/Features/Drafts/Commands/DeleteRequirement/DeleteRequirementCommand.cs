using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.DeleteRequirement
{
    public class DeleteRequirementCommand : IRequest
    {
        public int DraftId { get; set; }
        public int EndorsementId { get; set; }

        public DeleteRequirementCommand(int draftId, int endorsementId)
        {
            DraftId = draftId;
            EndorsementId = endorsementId;
        }
    }

}