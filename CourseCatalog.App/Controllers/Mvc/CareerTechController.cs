using System.Web.Mvc;

namespace CourseCatalog.App.Controllers.Mvc
{
    [RoutePrefix("careertech")]
    public class CareerTechController : Controller
    {
        [Route("clusters")]
        public ActionResult Clusters()
        {
            return View();
        }

        [Route("programs")]
        public ActionResult Programs()
        {
            return View();
        }

        [Route("credentials")]
        public ActionResult Credentials()
        {
            return View();
        }
    }
}