using System.Web.Mvc;

namespace CourseCatalog.App.Controllers.Mvc
{
    [RoutePrefix("courses")]
    public class CoursesController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("active")]
        public ActionResult ActiveCourses()
        {
            ViewBag.Filter = "active";
            return View("Index");
        }

        [Route("{courseId}")]
        public ActionResult Details(string courseId)
        {
            return View((object)courseId);
        }

        [Route("endorsements")]
        public ActionResult EndorsementCourses()
        {
            return View();
        }

    }
}