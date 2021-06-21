using MediatR;

namespace CourseCatalog.App.Features.Refs.ClusterTypes.Commands.UpdateClusterType
{
    public class UpdateClusterTypeCommand : IRequest
    {
        public int ClusterTypeId { get; set; }
        public string Name { get; set; }
        public string ClusterTypeCode { get; set; }
    }
}