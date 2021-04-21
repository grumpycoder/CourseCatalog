using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface IProgramTypeRepository : IAsyncRepository<ProgramType>
    {
        Task<ProgramType> GetProgramTypeByName(string name);
        Task<bool> HasPrograms(int clusterTypeId);
    }
}