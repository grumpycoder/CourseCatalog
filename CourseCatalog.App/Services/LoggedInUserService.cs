using System;
using System.Security.Claims;
using System.Web;
using CourseCatalog.App.Helpers;
using CourseCatalog.Application.Contracts;

namespace CourseCatalog.App.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService(HttpContext context)
        {
            var identity = (ClaimsIdentity) context.User.Identity;

            UserId = identity.GetClaimValue("emailaddress");

            if (identity.HasClaim(ClaimTypes.NameIdentifier))
                IdentityGuid = new Guid(identity.GetClaimValue(ClaimTypes.NameIdentifier));
        }

        public string UserId { get; }
        public Guid IdentityGuid { get; set; }
    }
}