using System.Collections.Generic;

namespace CourseCatalog.App.Features.Programs.Queries.GetProgramDetail
{
    public class ProgramDetailDto
    {
        public int ProgramId { get; set; }
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }

        public bool? TraditionalForMales { get; set; }
        public bool? TraditionalForFemales { get; set; }

        public int ProgramTypeId { get; set; }
        public string ProgramType { get; set; }

        public string Cluster { get; set; }
        public int ClusterId { get; set; }

        public List<ProgramCredentialListDto> Credentials { get; set; }
    }

    public class ProgramCredentialListDto
    {
        public int ProgramCredentialId { get; set; }
        public int CredentialId { get; set; }
        public string CredentialCode { get; set; }
        public string CredentialName { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }
    }
}