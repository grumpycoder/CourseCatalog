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

        [Route("endorsement-courses")]
        public ActionResult EndorsementCourses()
        {
            return View();
        }

        [Route("course-teachers")]
        public ActionResult CourseTeachers()
        {
            return View();
        }

        [Route("teacher-courses")]
        public ActionResult TeacherCourses()
        {
            return View();
        }

    }
}