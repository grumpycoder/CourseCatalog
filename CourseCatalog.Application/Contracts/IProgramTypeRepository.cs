using CourseCatalog.Domain.Entities;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface IProgramTypeRepository : IAsyncRepository<ProgramType>
    {
        Task<ProgramType> GetProgramTypeByName(string name);
        Task<bool> HasPrograms(int clusterTypeId);
    }
}
