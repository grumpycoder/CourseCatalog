using System;
using Alsde.Security.Identity;
using CourseCatalog.App.Helpers;
using CourseCatalog.Application.Contracts;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CourseCatalog.App.Controllers.Mvc
{
    public class AccountController : Controller
    {
        private readonly IMemberService _memberService;

        public AccountController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public async Task<ActionResult> LoginCallback(string token)
        {
            var tokenKey = new TokenKey(token, Constants.TpaAccessKey);

            var result = IdentityManager.TokenSignin(Constants.WebServiceUrl, tokenKey);

            Session["LoginFailureMessage"] = string.Empty;
            if (result.IsFailure)
            {
                Session["LoginFailureMessage"] = result.Error;
                ViewBag.Message = result.Error;
                return View("LoginFailure");
            }

            var identity = result.Value;
            if (identity == null)
            {
                ViewBag.Message = "User account not found";
                return View("LoginFailure");
            }

            var guid = identity.GetClaimValue(ClaimTypes.NameIdentifier);
            var groups = await _memberService.GetUserGroups(new Guid(guid));
            await _memberService.SyncClaims(identity);
            return RedirectToAction("Index", "Home");

        }

        public ActionResult SignOut()
        {
            IdentityManager.IdentitySignout();

            var logoutUrl = $"{Constants.LogoutUrl}{Constants.AimApplicationViewKey}";

            return Redirect(logoutUrl);
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

    }
}