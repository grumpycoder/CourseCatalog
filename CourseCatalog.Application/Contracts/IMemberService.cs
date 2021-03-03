using CourseCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseCatalog.Application.Contracts
{
    public interface IMemberService
    {
        Task<List<Group>> GetUserGroups(Guid identityGuid);
        Task SyncClaims(ClaimsIdentity identity);
    }
}
