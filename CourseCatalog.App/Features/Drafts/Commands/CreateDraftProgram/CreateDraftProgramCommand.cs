using MediatR;

namespace CourseCatalog.App.Features.Drafts.Commands.CreateDraftProgram
{
    public class CreateDraftProgramCommand : IRequest<CreatedDraftProgramDto>
    {
        public int DraftId { get; set; }
        public int ProgramId { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
    }

    public class CreatedDraftProgramDto
    {
        public int ProgramDraftId { get; set; }
        public int ProgramId { get; set; }
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
    }

}