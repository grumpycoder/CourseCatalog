using System.Collections.Generic;
using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities
{
    public class Program : AuditableEntity
    {
        public int ProgramId { get; set; }
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }

        public bool? TraditionalForMales { get; set; }
        public bool? TraditionalForFemales { get; set; }

        public int? ProgramTypeId { get; set; }

        public ProgramType ProgramType { get; set; }
        public int ClusterId { get; set; }

        public Cluster Cluster { get; set; }

        public List<ProgramCredential> Credentials { get; set; }
    }
}