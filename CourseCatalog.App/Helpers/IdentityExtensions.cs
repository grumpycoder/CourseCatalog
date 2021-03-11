using CourseCatalog.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace CourseCatalog.App.Helpers
{
    public static class IdentityExtensions
    {
        private const string Key = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

        public static void AddUpdateClaim(this IIdentity currentPrincipal, string key, string value)
        {
            if (!(currentPrincipal is ClaimsIdentity identity))
                return;

            // check for existing claim and remove it
            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);

            // add new claim
            identity.AddClaim(new Claim(key, value));
        }

        public static string GetClaimValue(this IIdentity currentPrincipal, string key)
        {
            var identity = currentPrincipal as ClaimsIdentity;

            var claim = identity?.Claims.FirstOrDefault(c => c.Type.ToLower().Contains(key.ToLower()));
            return claim?.Value;
        }

        public static string GetClaimValue(this ClaimsPrincipal currentPrincipal, string key)
        {
            var identity = currentPrincipal;
            var claim = identity?.Claims.FirstOrDefault(c => c.Type.ToLower().Contains(key));
            return claim?.Value;
        }

        public static bool HasRoleClaim(this IIdentity currentPrincipal, string role)
        {
            var identity = currentPrincipal as ClaimsIdentity;

            var claim = identity?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role && c.Value == role);

            return claim != null;
        }
        
        public static bool HasClaim(this IIdentity currentPrincipal, string key)
        {
            var identity = currentPrincipal as ClaimsIdentity;

            var claim = identity?.Claims.FirstOrDefault(c => c.Type.ToLower().Contains(key));

            return claim != null;
        }

        public static string GetEmailAddressClaim(this IIdentity currentPrincipal)
        {
            return GetClaimValue(ClaimsPrincipal.Current, Key).ToLower();
        }

        public static string AddGroupsToRoles(this IIdentity currentPrincipal, List<Group> groups)
        {
            var identity = currentPrincipal as ClaimsIdentity;

            foreach (var group in groups)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, @group.Name));
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, @group.Name));
            }
            return string.Empty;
        }
    }
}
