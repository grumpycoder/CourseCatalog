using System.Web.Mvc;

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

        //[Route("{id:int}/edit"), CustomAuthorize(Roles = "CourseAdmin")]
        [Route("{id:int}/edit")]
        public ActionResult Edit(int id)
        {
            return View(id);
        }

        //[Route("new"), CustomAuthorize(Roles = "CourseAdmin")]
        [Route("new")]
        public ActionResult New()
        {
            return View("Edit", -1);
        }
    }
}