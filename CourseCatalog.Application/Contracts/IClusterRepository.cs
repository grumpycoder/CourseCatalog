using System.Collections.Generic;
using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface IClusterRepository : IAsyncRepository<Cluster>
    {
        Task<List<Cluster>> GetClustersWithDetails();
        Task<Cluster> GetClusterWithDetails(int id);
        Task<Cluster> GetClusterByClusterCode(string clusterCode);
    }
}