using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Alsde.Extensions;
using Microsoft.Data.SqlClient;

namespace CourseCatalog.App.Helpers
{
    public static partial class MvcWebExtensions
    {
        public static IHtmlString RenderApplicationName(this HtmlHelper htmlHelper)
        {
            var appInstance = htmlHelper.ViewContext.HttpContext.ApplicationInstance;

            var memberInfo = appInstance.GetType().BaseType;
            if (memberInfo != null)
            {
                var attr = memberInfo.Assembly.GetAssemblyAttribute<AssemblyTitleAttribute>();

                return new MvcHtmlString(attr.Title ?? "No Application Title");
            }
            return new MvcHtmlString("No Application Title");
        }

        public static IHtmlString RenderApplicationDescription(this HtmlHelper htmlHelper)
        {
            var appInstance = htmlHelper.ViewContext.HttpContext.ApplicationInstance;

            var memberInfo = appInstance.GetType().BaseType;
            if (memberInfo != null)
            {
                var attr = memberInfo.Assembly.GetAssemblyAttribute<AssemblyDescriptionAttribute>();

                return new MvcHtmlString(attr.Description ?? "No Application Description");
            }
            return new MvcHtmlString("No Application Description");
        }

        public static IHtmlString RenderMachineName(this HtmlHelper htmlHelper)
        {
            var value = System.Environment.MachineName;
            return new MvcHtmlString(value);
        }

        public static IHtmlString RenderDataSource(this HtmlHelper htmlHelper)
        {
            var connectionString = AppSettings.GetDatabaseString<string>(Constants.DatabaseContextName);
            var builder = new SqlConnectionStringBuilder { ConnectionString = connectionString };
            return new MvcHtmlString(builder.DataSource);
        }

        public static IHtmlString RenderDatabaseName(this HtmlHelper htmlHelper)
        {
            var connectionString = AppSettings.GetDatabaseString<string>(Constants.DatabaseContextName);
            var builder = new SqlConnectionStringBuilder { ConnectionString = connectionString };
            return new MvcHtmlString(builder.InitialCatalog);
        }

        public static IHtmlString RenderEnvironmentStatusBarColor(this HtmlHelper htmlHelper, string environment)
        {
            var cssClass = GetCssClass(environment);

            return MvcHtmlString.Create(cssClass);
        }

        public static string RenderUserFullname(this HtmlHelper htmlHelper)
        {
            var fullname = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims
                .FirstOrDefault(c => c.Type == "FullName")?.Value.ToTitleCase();
            var name = ((ClaimsIdentity)HttpContext.Current.User.Identity).Name.ToLower().ToTitleCase();
            return fullname ?? name;
        }

        public static string RenderClaim(this HtmlHelper htmlHelper, string key)
        {
            var claim = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims
                .FirstOrDefault(c => c.Type == key)?.Value;
            return claim;
        }

        private static string GetCssClass(string environment)
        {
            string cssClass = "bg-light";

            switch (environment.ToLower())
            {
                case "development":
                    cssClass = "bg-orange";
                    break;
                case "dev":
                    cssClass = "bg-orange";
                    break;
                case "test":
                    cssClass = "bg-blue";
                    break;
                case "stage":
                    cssClass = "bg-yellow";
                    break;
            }

            return cssClass;
        }
        public static void GetHttpStatus(Exception ex, out int httpStatus)
        {
            httpStatus = 500;  // default is server error
            if (ex is HttpException)
            {
                var httpEx = ex as HttpException;
                httpStatus = httpEx.GetHttpCode();
            }
        }

        public static bool IsDev()
        {
            return AppSettings.Get<string>("ASPNET_ENV") == "Dev";
        }

    }
}
