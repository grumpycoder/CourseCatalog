using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class ClusterTypeRepository : BaseRepository<ClusterType>, IClusterTypeRepository
    {
        public ClusterTypeRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ClusterType> GetClusterTypeByName(string name)
        {
            return await _dbContext.ClusterTypes.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<bool> HasClusters(int clusterTypeId)
        {
            return await _dbContext.Clusters.AnyAsync(c => c.ClusterTypeId == clusterTypeId);
        }
    }
}
