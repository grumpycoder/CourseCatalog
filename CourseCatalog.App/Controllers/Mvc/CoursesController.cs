using System.Web.Mvc;
using CourseCatalog.App.Filters;

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
        [Authorize()]
        public ActionResult EndorsementCourses()
        {
            return View();
        }

        [Route("course-teachers")]
        [CustomAuthorize(Roles = "CourseAdmin, TeacherCertAdmin, CareerTechAdmin, Admin")]
        public ActionResult CourseTeachers()
        {
            return View();
        }

        [Route("teacher-courses")]
        [CustomAuthorize(Roles = "CourseAdmin, TeacherCertAdmin, CareerTechAdmin, Admin")]
        public ActionResult TeacherCourses()
        {
            return View();
        }
    }
}