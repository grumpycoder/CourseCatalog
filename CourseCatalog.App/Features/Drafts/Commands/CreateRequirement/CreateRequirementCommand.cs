using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.CreateRequirement
{
    public class CreateRequirementCommand : IRequest<CreatedDraftEndorsementDto>
    {
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