using System.Collections.Generic;
using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface IProgramRepository : IAsyncRepository<Program>
    {
        Task<List<Program>> GetProgramListWithDetails();
        Task<List<Program>> GetProgramsWithDetails();
        Task<Program> GetProgramByIdWithDetails(int programId);
        Task<Program> GetProgramByProgramCode(string programCode);
    }
}