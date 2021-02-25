using MyDemo.Api.Domain.Entities;

namespace MyDemo.Api.Features.Credentials.Queries.GetCredentialDetail
{
    public class CredentialDetailVm
    {
        public int CredentialId { get; set; }
        public string CredentialCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool? IsReimbursable { get; set; }

        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }

        public int CredentialTypeId { get; set; }
        public CredentialType CredentialType { get; set; }
    }

}