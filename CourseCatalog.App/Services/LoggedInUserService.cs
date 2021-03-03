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
        }

        public string UserId { get; }
    }
}
