using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities
{
    public class ProgramDraft: AuditableEntity
    {
        public int ProgramDraftId { get; set; }
        public int DraftId { get; set; }
        public int ProgramId { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
        public Draft Draft { get; set; }
        public Program Program { get; set; }
    }
}