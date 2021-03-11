using System;
using System.Web.Mvc;

namespace CourseCatalog.App.Controllers.Mvc
{
    [RoutePrefix("tests")]
    public class TestsController : Controller
    {
        // GET: Tests
        public ActionResult Index()
        {
            return View();
        }

        [Route("ThrowError")]
        public ActionResult ThrowError()
        {
            throw new Exception("Test Error message should be logged");
            return View();
        }
    }
}