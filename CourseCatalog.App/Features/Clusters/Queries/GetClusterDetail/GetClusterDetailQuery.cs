using MediatR;

namespace CourseCatalog.App.Features.Clusters.Queries.GetClusterDetail
{
    public class GetClusterDetailQuery : IRequest<ClusterDetailDto>
    {
        public GetClusterDetailQuery(int clusterId)
        {
            ClusterId = clusterId;
        }

        public int ClusterId { get; set; }
    }
}