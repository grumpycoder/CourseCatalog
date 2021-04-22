using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.CreateDraftRequirement
{
    public class CreateDraftEndorsementCommand : IRequest<CreatedDraftEndorsementDto>
    {
        public CreateDraftEndorsementCommand(int draftId, int endorsementId)
        {
            DraftId = draftId;
            EndorsementId = endorsementId;
        }

        public int DraftId { get; set; }
        public int EndorsementId { get; set; }
    }

    public class CreatedDraftEndorsementDto
    {
        public int DraftEndorsementId { get; set; }
        public int EndorsementId { get; set; }
        public string EndorseCode { get; set; }
        public string Description { get; set; }
    }
}