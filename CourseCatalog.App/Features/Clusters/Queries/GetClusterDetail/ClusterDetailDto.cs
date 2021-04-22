using System.Collections.Generic;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.App.Features.Clusters.Queries.GetClusterDetail
{
    public class ClusterDetailDto
    {
        public int ClusterId { get; set; }
        public string ClusterCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EdFactsClusterValue { get; set; }

        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }

        public int? ClusterTypeId { get; set; }
        public ClusterType ClusterType { get; set; }

        public List<ClusterProgramListDto> Programs { get; set; }
    }

    public class ClusterProgramListDto
    {
        public int ProgramId { get; set; }
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public int BeginYear { get; set; }
        public int EndYear { get; set; }
    }
}