using MediatR;

namespace CourseCatalog.App.Features.Clusters.Queries.GetClusterDetail
{
    public class GetClusterDetailQuery : IRequest<ClusterDetailDto>
    {
        public int ClusterId { get; set; }

        public GetClusterDetailQuery(int clusterId)
        {
            ClusterId = clusterId;
        }
    }
}
