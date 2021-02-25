using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class ClusterRepository : BaseRepository<Cluster>, IClusterRepository
    {
        public ClusterRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Cluster>> GetClustersWithDetails()
        {
            var clusters = await _dbContext.Clusters
                .Include(c => c.ClusterType)
                .ToListAsync();

            return clusters;
        }

        public async Task<Cluster> GetClusterWithDetails(int id)
        {
            var cluster = await _dbContext.Clusters
                .Include(c => c.ClusterType)
                .FirstOrDefaultAsync(x => x.ClusterId == id);

            return cluster;
        }

        public async Task<Cluster> GetClusterByClusterCode(string clusterCode)
        {
            return await _dbContext.Clusters.FirstOrDefaultAsync(c => c.ClusterCode == clusterCode);
        }
    }
}