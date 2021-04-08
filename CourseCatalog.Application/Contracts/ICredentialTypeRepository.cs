using CourseCatalog.Domain.Entities;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface ICredentialTypeRepository : IAsyncRepository<CredentialType>
    {
        Task<CredentialType> GetCredentialTypeByName(string name);
        Task<bool> HasCredentials(int clusterTypeId);
    }
}
