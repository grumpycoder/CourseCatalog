using CourseCatalog.App.Filters;
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

        [Route("{courseNumber}/edit"), CustomAuthorize(Roles = "CourseAdmin")]
        public ActionResult Edit(string courseNumber)
        {
            return View((object)courseNumber);
        }


        [Route("draft"), Authorize(Roles = "CourseAdmin")]
        public ActionResult Draft()
        {
            //TODO: Need draft workflow
            var courseCode = string.Empty;
            return View("Edit", (object)courseCode);
        }

        [Route("requests")]
        public ActionResult Requests()
        {
            ViewBag.Message = "Courses Requests have not been implemented yet.";
            return View("ComingSoon");
        }

    }
}