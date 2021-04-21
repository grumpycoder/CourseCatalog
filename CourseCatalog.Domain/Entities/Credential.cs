using System.Collections.Generic;
using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities
{
    public class Credential : AuditableEntity
    {
        public int CredentialId { get; set; }
        public string CredentialCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsReimbursable { get; set; }

        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
        public int? CredentialTypeId { get; set; }

        public CredentialType CredentialType { get; set; }

        public List<ProgramCredential> Programs { get; set; }
    }
}