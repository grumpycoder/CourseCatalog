using CourseCatalog.Domain.Entities;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface IClusterTypeRepository : IAsyncRepository<ClusterType>
    {
        Task<ClusterType> GetClusterTypeByName(string name);
        Task<bool> HasClusters(int clusterTypeId);
    }
}