namespace CourseCatalog.Domain.Entities
{
    public class CredentialType
    {
        public int CredentialTypeId { get; set; }
        public string CredentialTypeCode { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
    }
}