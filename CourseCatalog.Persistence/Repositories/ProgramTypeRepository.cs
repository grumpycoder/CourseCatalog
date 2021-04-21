using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class ProgramTypeRepository : BaseRepository<ProgramType>, IProgramTypeRepository
    {
        public ProgramTypeRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ProgramType> GetProgramTypeByName(string name)
        {
            return await _dbContext.ProgramTypes.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<bool> HasPrograms(int clusterTypeId)
        {
            return await _dbContext.Programs.AnyAsync(c => c.ProgramTypeId == clusterTypeId);
        }
    }
}