using System.Web.Mvc;

namespace CourseCatalog.App.Helpers
{
    public static class NavigationHelper
    {

        public static MvcHtmlString BsNavigationLink(this HtmlHelper html, string linkText, string action, string controller, string icon = null, object routeValues = null, object css = null)
        {
            var activeRouteAction = html.ViewContext.RouteData.Values["action"].ToString();
            var activeRouteController = html.ViewContext.RouteData.Values["controller"].ToString();

            var aTag = new TagBuilder("a");
            var liTag = new TagBuilder("li");
            var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(css);
            var url = routeValues == null ?
                new UrlHelper(html.ViewContext.RequestContext).Action(action, controller)
                : new UrlHelper(html.ViewContext.RequestContext).Action(action, controller, routeValues);

            var imageIcon = string.Empty;
            if (icon != null)
            {
                imageIcon = $@"<i class='{icon} visible-sm'>&nbsp;</i>&nbsp;&nbsp;";
            }

            linkText = imageIcon + "<span class='hidden-sm'>" + linkText + "<span>";

            aTag.AddCssClass("nav-link");

            aTag.MergeAttribute("href", url);
            aTag.InnerHtml = linkText;

            aTag.MergeAttributes(htmlAttributes);

            liTag.AddCssClass("nav-item");

            if (activeRouteController == controller || activeRouteController == controller && activeRouteAction == action)
            {
                liTag.AddCssClass("active");
            }

            liTag.InnerHtml = aTag.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(liTag.ToString(TagRenderMode.Normal));
        }

        public static string IsLinkActive(this UrlHelper url, string action, string controller)
        {
            var activeRouteAction = url.RequestContext.RouteData.Values["action"].ToString();
            var activeRouteController = url.RequestContext.RouteData.Values["controller"].ToString();

            if (activeRouteController == controller || activeRouteController == controller && activeRouteAction == action)
            {
                return "active";
            }

            return "";
        }


    }
}
