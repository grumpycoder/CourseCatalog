using Alsde.Security.Identity;
using CourseCatalog.Application.Contracts;
using System;
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

            try
            {
                var tokenSignin = IdentityManager.TokenSignin(Constants.WebServiceUrl, tokenKey, Constants.AimApplicationViewKey);

                Session["LoginFailureMessage"] = string.Empty;
                if (tokenSignin.IsFailure)
                {
                    Session["LoginFailureMessage"] = tokenSignin.Error;
                    ViewBag.Message = tokenSignin.Error;
                    return View("LoginFailure");
                }

                var identity = tokenSignin.Value;
                if (identity == null)
                {
                    ViewBag.Message = "User account not found";
                    return View("LoginFailure");
                }

                await _memberService.SyncClaims(identity);
            }
            catch (Exception e)
            {
                if (!string.IsNullOrEmpty(e.InnerException?.Message))
                    throw new Exception(e.InnerException?.Message, e.InnerException);

                throw new Exception(e.InnerException?.ToString(), e.InnerException);
            }
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