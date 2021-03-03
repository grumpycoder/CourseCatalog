using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetClusterTypetList
{
    public class GetClusterTypeListQuery : IRequest<List<ClusterType>>
    {

    }
}
