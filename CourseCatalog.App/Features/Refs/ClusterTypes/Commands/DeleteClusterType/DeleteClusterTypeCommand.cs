using MediatR;

namespace CourseCatalog.App.Features.Refs.ClusterTypes.Commands.DeleteClusterType
{
    public class DeleteClusterTypeCommand : IRequest
    {
        public DeleteClusterTypeCommand(int clusterTypeId)
        {
            ClusterTypeId = clusterTypeId;
        }

        public int ClusterTypeId { get; set; }
    }
}