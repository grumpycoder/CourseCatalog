using CourseCatalog.App.Filters;
using System.Web.Mvc;

namespace CourseCatalog.App.Controllers.Mvc
{
    [RoutePrefix("settings"), CustomAuthorize(Roles = "CourseAdmin, TeacherCertAdmin, CareerTechAdmin, Admin")]
    public class SettingsController : Controller
    {
        [Route("subjects")]
        public ActionResult Subjects()
        {
            return View();
        }

        [Route("courselevels")]
        public ActionResult CourseLevels()
        {
            return View();
        }

        [Route("deliverytypes")]
        public ActionResult DeliveryTypes()
        {
            return View();
        }

        [Route("credittypes")]
        public ActionResult CreditTypes()
        {
            return View();
        }

        [Route("clustertypes")]
        public ActionResult ClusterTypes()
        {
            return View();
        }

        [Route("programtypes")]
        public ActionResult ProgramTypes()
        {
            return View();
        }

        [Route("credentialtypes")]
        public ActionResult CredentialTypes()
        {
            return View();
        }

    }
}