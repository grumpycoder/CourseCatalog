using System.Web.Http;
using System.Web.Mvc;
using CourseCatalog.App.Areas.HelpPage.App_Start;

namespace CourseCatalog.App.Areas.HelpPage
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName => "HelpPage";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default",
                "Help/{action}/{apiId}",
                new {controller = "Help", action = "Index", apiId = UrlParameter.Optional});

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}