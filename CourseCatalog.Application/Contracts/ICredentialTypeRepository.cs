using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface ICredentialTypeRepository : IAsyncRepository<CredentialType>
    {
        Task<CredentialType> GetCredentialTypeByName(string name);
        Task<bool> HasCredentials(int clusterTypeId);
    }
}