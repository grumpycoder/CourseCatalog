using System.Web.Mvc;
using CourseCatalog.App.Filters;

namespace CourseCatalog.App.Controllers.Mvc
{
    [RoutePrefix("admin")]
    [CustomAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        [Route("groups")]
        public ActionResult Groups()
        {
            return View();
        }

        [Route("users")]
        public ActionResult Users()
        {
            ViewBag.Message = "User Management has not been implemented yet.";
            return View("ComingSoon");
        }

        [Route("error-log")]
        public ActionResult ErrorViewer()
        {
            return View();
        }

        [Route("perf-log")]
        public ActionResult PerfViewer()
        {
            return View();
        }
    }
}