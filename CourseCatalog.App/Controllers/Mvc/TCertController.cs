using System.Web.Mvc;

namespace CourseCatalog.App.Controllers.Mvc
{
    [RoutePrefix("tcert")]
    public class TCertController : Controller
    {
        [Route("course-requirements")] //, CustomAuthorize(Roles = "TeacherCertAdmin")]
        public ActionResult CourseRequirements()
        {
            return View();
        }
    }
}