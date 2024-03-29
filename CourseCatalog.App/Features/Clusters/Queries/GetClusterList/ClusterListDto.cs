﻿namespace CourseCatalog.App.Features.Clusters.Queries.GetClusterList
{
    public class ClusterListDto
    {
        public int ClusterId { get; set; }
        public string ClusterCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EdFactsClusterValue { get; set; }

        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }

        public int ClusterTypeId { get; set; }
        public string ClusterTypeName { get; set; }
        public string ClusterTypeCode { get; set; }
    }
}