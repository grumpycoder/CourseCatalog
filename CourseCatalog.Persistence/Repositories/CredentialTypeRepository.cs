using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class CredentialTypeRepository : BaseRepository<CredentialType>, ICredentialTypeRepository
    {
        public CredentialTypeRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<CredentialType> GetCredentialTypeByName(string name)
        {
            return await _dbContext.CredentialTypes.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<bool> HasCredentials(int clusterTypeId)
        {
            return await _dbContext.Credentials.AnyAsync(c => c.CredentialTypeId == clusterTypeId);
        }
    }
}