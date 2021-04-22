using System.Web.Mvc;
using CourseCatalog.App.Filters;

namespace CourseCatalog.App.Controllers.Mvc
{
    [RoutePrefix("drafts")]
    public class DraftsController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("{id:int}")]
        public ActionResult Detail(int id)
        {
            return View(id);
        }

        [Route("{id:int}/edit")]
        [CustomAuthorize(Roles = "CourseAdmin, TeacherCertAdmin, CareerTechAdmin, Admin")]
        public ActionResult Edit(int id)
        {
            return View(id);
        }

        [Route("new")]
        [CustomAuthorize(Roles = "CourseAdmin, Admin")]
        public ActionResult New()
        {
            return View("Edit", -1);
        }
    }
}