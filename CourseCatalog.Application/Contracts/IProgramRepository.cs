using CourseCatalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface IProgramRepository : IAsyncRepository<Program>
    {
        Task<List<Program>> GetProgramListWithDetails();
    }
}