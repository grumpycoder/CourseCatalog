using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface IClusterTypeRepository : IAsyncRepository<ClusterType>
    {
        Task<ClusterType> GetClusterTypeByName(string name);
        Task<bool> HasClusters(int clusterTypeId);
    }
}