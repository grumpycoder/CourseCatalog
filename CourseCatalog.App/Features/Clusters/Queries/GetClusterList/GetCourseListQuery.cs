using System.Collections.Generic;
using MediatR;

namespace CourseCatalog.App.Features.Clusters.Queries.GetClusterList
{
    public class GetClusterListQuery : IRequest<List<ClusterListDto>>
    {
    }
}