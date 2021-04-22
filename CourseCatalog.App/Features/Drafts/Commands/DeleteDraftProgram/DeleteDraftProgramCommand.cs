using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.DeleteDraftProgram
{
    public class DeleteDraftProgramCommand : IRequest
    {
        public DeleteDraftProgramCommand(int draftId, int programId)
        {
            DraftId = draftId;
            ProgramId = programId;
        }

        public int DraftId { get; set; }
        public int ProgramId { get; set; }
    }
}