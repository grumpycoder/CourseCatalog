using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands
{
    public class DeleteRequirementCommand : IRequest
    {
        public int DraftId { get; set; }
        public int EndorsementId { get; set; }
    }

}