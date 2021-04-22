using System.Web.Mvc;

namespace CourseCatalog.App.Filters
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            var request = httpContext.Request;
            var response = httpContext.Response;

            if (request.IsAjaxRequest())
            {
                response.SuppressFormsAuthenticationRedirect = true;
                base.HandleUnauthorizedRequest(filterContext);
            }
            //TODO: Refactor Authorized filter
            if (httpContext.User.Identity.IsAuthenticated)
                filterContext.Result = new RedirectResult("~/Account/Unauthorized");
            else
                filterContext.Result = new RedirectResult("~/Account/Unauthorized");
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (AuthorizeCore(filterContext.HttpContext))
                base.OnAuthorization(filterContext);
            else
                HandleUnauthorizedRequest(filterContext);
        }
    }
}