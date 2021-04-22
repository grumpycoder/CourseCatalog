using CourseCatalog.App.Helpers;

namespace CourseCatalog.App
{
    public static class Constants
    {
        public const string ApplicationName = "Course Catalog";
        public const string LayerName = "WebApp";
        public const string WebApiLayerName = "WebApi";

        public static string LogoutUrl = AppSettings.Get<string>("LogoutUrl");
        public static string LoginUrl = AppSettings.Get<string>("LoginUrl");

        public static string DatabaseContextName => "CourseContext";
        public static string TpaAccessKey => AppSettings.Get<string>("TPA_AccessKey");
        public static string WebServiceUrl => AppSettings.Get<string>("WebServiceUrl");
        public static string AimBaseUrl => AppSettings.Get<string>("LoginUrl"); //"
        public static string AimApplicationViewKey => AppSettings.Get<string>("ALSDE_AIM_ApplicationViewKey");
        public static string Environment => AppSettings.Get<string>("ASPNET_ENV");

        public static string GlobalAdministrators => AppSettings.Get<string>("GlobalAdministratorsGroupName");
    }
}