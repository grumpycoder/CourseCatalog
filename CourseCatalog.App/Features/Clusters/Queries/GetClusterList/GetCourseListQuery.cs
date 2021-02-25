using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Clusters.Queries.GetClusterList
{
    public class GetClusterListQuery : IRequest<List<ClusterListDto>>
    {

    }
}
