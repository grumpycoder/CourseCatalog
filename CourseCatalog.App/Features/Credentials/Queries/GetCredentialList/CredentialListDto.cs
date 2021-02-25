namespace CourseCatalog.App.Features.Credentials.Queries.GetCredentialList
{
    public class CredentialListDto
    {
        public int CredentialId { get; set; }
        public string CredentialCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool? IsReimbursable { get; set; }

        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }

        public int CredentialTypeId { get; set; }
        public string CredentialType { get; set; }
        public string CredentialTypeCode { get; set; }


    }


}