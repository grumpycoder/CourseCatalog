using CourseCatalog.App.Filters;
using System.Web.Mvc;

namespace CourseCatalog.App.Controllers.Mvc
{
    [RoutePrefix("settings")]
    [CustomAuthorize(Roles = "CourseAdmin, TeacherCertAdmin, CareerTechAdmin, Admin")]
    public class SettingsController : Controller
    {
        [Route("subjects")]
        public ActionResult Subjects()
        {
            return View();
        }

        [Route("courseLevels")]
        public ActionResult CourseLevels()
        {
            return View();
        }

        [Route("deliveryTypes")]
        public ActionResult DeliveryTypes()
        {
            return View();
        }

        [Route("creditTypes")]
        public ActionResult CreditTypes()
        {
            return View();
        }

        [Route("clusterTypes")]
        public ActionResult ClusterTypes()
        {
            return View();
        }

        [Route("programTypes")]
        public ActionResult ProgramTypes()
        {
            return View();
        }

        [Route("credentialTypes")]
        public ActionResult CredentialTypes()
        {
            return View();
        }
    }
}