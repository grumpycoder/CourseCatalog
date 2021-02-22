namespace CourseCatalog.Domain.Entities
{
    public class ProgramCredential
    {
        public int ProgramId { get; set; }
        public int CredentialId { get; set; }
        public Program Program { get; set; }
        public Credential Credential { get; set; }
    }
}