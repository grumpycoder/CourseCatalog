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
        public ActionResult ClusterDetail(int clusterId)
        {
            return View("ClusterDetail", clusterId);
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

        [Route("programs/{programId}")]
        public ActionResult ProgramDetail(int programId)
        {
            return View("ProgramDetail", programId);
        }

        [Route("programs/{programId}/edit")] //,CustomAuthorize(Roles = "CareerTechAdmin")]
        public ActionResult ProgramEdit(int programId)
        {
            return View(programId);
        }

        [Route("credentials")]
        public ActionResult Credentials()
        {
            return View();
        }
    }
}