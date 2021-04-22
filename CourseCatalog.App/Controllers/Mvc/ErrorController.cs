using System.Web.Mvc;

namespace CourseCatalog.App.Controllers.Mvc
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Error = Session["Error"];
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}