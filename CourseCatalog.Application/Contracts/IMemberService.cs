using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.Application.Contracts
{
    public interface IMemberService
    {
        Task<List<Group>> GetUserGroups(Guid identityGuid);
        Task SyncClaims(ClaimsIdentity identity);
    }
}