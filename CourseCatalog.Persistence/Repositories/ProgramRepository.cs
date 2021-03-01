using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class ProgramRepository : BaseRepository<Program>, IProgramRepository
    {
        public ProgramRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Program>> GetProgramListWithDetails()
        {
            return await _dbContext.Programs
                .Include(c => c.Cluster)
                .Include(c => c.ProgramType)
                .ToListAsync();
        }

        public async Task<List<Program>> GetProgramsWithDetails()
        {
            var programs = await _dbContext.Programs
                .Include(c => c.ProgramType)
                .ToListAsync();

            return programs;
        }
        public async Task<Program> GetProgramByIdWithDetails(int programId)
        {
            var program = await _dbContext.Programs
                .Include(c => c.ProgramType)
                .Include(c => c.Cluster)
                .Include(c => c.Credentials)
                .ThenInclude(x => x.Credential)
                .FirstOrDefaultAsync(x => x.ProgramId == programId);

            return program;
        }

        public async Task<Program> GetProgramByProgramCode(string programCode)
        {
            return await _dbContext.Programs.FirstOrDefaultAsync(c => c.ProgramCode == programCode);
        }
    }
}