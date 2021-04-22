using System.Web.Mvc;
using CourseCatalog.App.Filters;

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

        [Route("clusters/{clusterId?}/edit")]
        [CustomAuthorize(Roles = "CareerTechAdmin, Admin")]
        public ActionResult ClusterEdit(int clusterId)
        {
            return View(clusterId);
        }

        [Route("clusters/new")]
        [CustomAuthorize(Roles = "CareerTechAdmin, Admin")]
        public ActionResult ClusterNew()
        {
            return View("ClusterEdit", -1);
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

        [Route("programs/{programId}/edit")]
        [CustomAuthorize(Roles = "CareerTechAdmin, Admin")]
        public ActionResult ProgramEdit(int programId)
        {
            return View(programId);
        }

        [Route("programs/new")]
        [CustomAuthorize(Roles = "CareerTechAdmin, Admin")]
        public ActionResult ProgramCreate()
        {
            return View("ProgramEdit", -1);
        }

        [Route("credentials")]
        public ActionResult Credentials()
        {
            return View();
        }

        [Route("credentials/{credentialId}")]
        public ActionResult Credentials(int credentialId)
        {
            return View("CredentialDetail", credentialId);
        }

        [Route("credentials/{credentialId}/edit")]
        [CustomAuthorize(Roles = "CareerTechAdmin, Admin")]
        public ActionResult CredentialEdit(int credentialId)
        {
            return View(credentialId);
        }

        [Route("credentials/new")]
        [CustomAuthorize(Roles = "CareerTechAdmin, Admin")]
        public ActionResult CredentialCreate()
        {
            return View("CredentialEdit", -1);
        }
    }
}