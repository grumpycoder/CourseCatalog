using CourseCatalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface IClusterRepository : IAsyncRepository<Cluster>
    {
        Task<List<Cluster>> GetClustersWithDetails();
        Task<Cluster> GetClusterWithDetails(int id);
        Task<Cluster> GetClusterByClusterCode(string clusterCode);
    }
}