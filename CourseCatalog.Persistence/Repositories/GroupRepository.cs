using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {

        public GroupRepository(CourseDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Group> GetGroupByIdWithUsers(int groupId)
        {
            return await _dbContext.Groups
                .Include(ug => ug.Users).ThenInclude(u => u.User)
                .FirstOrDefaultAsync(g => g.Id == groupId);
        }

        public async Task<List<Group>> GetGroupsWithUsers()
        {
            return await _dbContext.Groups
                .Include(ug => ug.Users).ThenInclude(u => u.User)
                .ToListAsync();
        }
    }
}
