using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities
{
    public class ProgramCredential : AuditableEntity
    {
        public int ProgramCredentialId { get; set; }
        public int ProgramId { get; set; }
        public int CredentialId { get; set; }
        public Program Program { get; set; }
        public Credential Credential { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
    }
}