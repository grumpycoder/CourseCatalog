using System.Collections.Generic;

namespace CourseCatalog.App.Features.Credentials.Queries.GetCredentialDetail
{
    public class CredentialDetailDto
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

        public List<CredentialProgramListDto> Programs { get; set; }
    }

    public class CredentialProgramListDto
    {
        public int ProgramCredentialId { get; set; }
        public int ProgramId { get; set; }
        public string ProgramCode { get; set; }
        public string ProgramName { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
    }
}