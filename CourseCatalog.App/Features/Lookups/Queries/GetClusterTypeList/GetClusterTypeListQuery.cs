using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Lookups.Queries.GetClusterTypeList
{
    public class GetClusterTypeListQuery : IRequest<List<ClusterType>>
    {
    }
}