using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<List<Tag>> GetCreditTypeTags()
        {
            var creditTypes = await _dbContext.Tags.Where(t => t.GroupName == "CreditType").ToListAsync();
            return creditTypes;
        }

        public async Task<List<Tag>> GetGeneralTags()
        {
            var creditTypes = await _dbContext.Tags.Where(t => t.GroupName == "General").ToListAsync();
            return creditTypes;
        }
    }
}