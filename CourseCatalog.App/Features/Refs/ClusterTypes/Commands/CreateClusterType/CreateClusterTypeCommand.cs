using MediatR;

namespace CourseCatalog.App.Features.Refs.ClusterTypes.Commands.CreateClusterType
{
    public class CreateClusterTypeCommand : IRequest<int>
    {
        public int ClusterTypeId { get; set; }
        public string Name { get; set; }
    }
}
