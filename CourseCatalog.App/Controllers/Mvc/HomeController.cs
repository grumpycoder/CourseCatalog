using System;
using System.Web.Mvc;

namespace CourseCatalog.App.Controllers.Mvc
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            throw new Exception("Testing error handling"); 
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
