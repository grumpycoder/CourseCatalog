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

        [Route("clusters/{clusterId?}")]
        public ActionResult ClusterDetails(int clusterId)
        {
            return View("ClusterDetails", clusterId);
        }

        [Route("clusters/{clusterId?}/edit")] //, CustomAuthorize(Roles = "CareerTechAdmin")]
        public ActionResult ClusterEdit(int clusterId)
        {
            return View(clusterId);
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