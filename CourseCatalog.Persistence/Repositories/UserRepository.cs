using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public UserRepository(CourseDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.IdentityGuid == userId);
        }

        public async Task<User> GetUserByIdWithDetails(Guid userId)
        {
            return await _dbContext.Users
                .Include(u => u.Groups)
                .ThenInclude(g => g.Group)
                .FirstOrDefaultAsync(u => u.IdentityGuid == userId);
        }
    }
}
