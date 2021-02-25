using System.Collections.Generic;
using System.Threading.Tasks;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalog.Persistence.Repositories
{
    public class CredentialRepository : BaseRepository<Credential>, ICredentialRepository
    {
        public CredentialRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Credential>> GetCredentialsWithDetails()
        {
            var credentials = await _dbContext.Credentials
                .Include(c => c.CredentialType)
                .ToListAsync();

            return credentials;
        }

        public async Task<Credential> GetCredentialWithDetails(int id)
        {
            var credential = await _dbContext.Credentials
                .Include(c => c.CredentialType)
                .FirstOrDefaultAsync(x => x.CredentialId == id);

            return credential;
        }

        public async Task<Credential> GetCredentialByCredentialCode(string credentialCode)
        {
            return await _dbContext.Credentials.FirstOrDefaultAsync(c => c.CredentialCode == credentialCode);
        }
    }
}