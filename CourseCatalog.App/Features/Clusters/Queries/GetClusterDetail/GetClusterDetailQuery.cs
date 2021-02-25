using MediatR;

namespace CourseCatalog.App.Features.Clusters.Queries.GetClusterDetail
{
    public class GetClusterDetailQuery : IRequest<ClusterDetailVm>
    {
        public int ClusterId { get; set; }
    }
}
