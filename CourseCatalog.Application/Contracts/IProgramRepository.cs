using CourseCatalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

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