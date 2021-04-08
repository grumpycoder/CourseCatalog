using MediatR;

namespace CourseCatalog.App.Features.Refs.ClusterTypes.Commands.DeleteClusterType
{
    public class DeleteClusterTypeCommand : IRequest
    {
        public int ClusterTypeId { get; set; }

        public DeleteClusterTypeCommand(int clusterTypeId)
        {
            ClusterTypeId = clusterTypeId;
        }
    }
}
