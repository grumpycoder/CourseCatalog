using System;
using CourseCatalog.App.Helpers;
using CourseCatalog.Application.Contracts;
using System.Security.Claims;
using System.Web;

namespace CourseCatalog.App.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService(HttpContext context)
        {
            var identity = (ClaimsIdentity)context.User.Identity;
            UserId = identity.GetClaimValue("emailaddress");
            IdentityGuid = new Guid(identity.GetClaimValue(ClaimTypes.NameIdentifier));
        }

        public string UserId { get; }
        public Guid IdentityGuid { get; set; }
    }
}
