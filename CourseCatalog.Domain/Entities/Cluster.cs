using System.Collections.Generic;
using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities
{
    public class Cluster : AuditableEntity
    {
        public int ClusterId { get; set; }
        public string ClusterCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EdFactsClusterValue { get; set; }

        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }

        public int ClusterTypeId { get; set; }
        public ClusterType ClusterType { get; set; }

        public List<Program> Programs { get; set; }
    }
}