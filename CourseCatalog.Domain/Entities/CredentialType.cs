using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities
{
    public class CredentialType : AuditableEntity
    {
        public int CredentialTypeId { get; set; }
        public string CredentialTypeCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}