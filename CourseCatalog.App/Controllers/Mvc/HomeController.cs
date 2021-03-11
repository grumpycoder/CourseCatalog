using System;
using System.Web.Mvc;

namespace CourseCatalog.App.Controllers.Mvc
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            throw new Exception("Test Exception");
            return View();
        }
    }
}
