using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CourseCatalog.App.Controllers.Mvc;
using CourseCatalog.App.Helpers;
using CourseCatalog.App.Services;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Persistence;
using CourseCatalog.Persistence.Repositories;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Enrichers.HttpContextData;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CourseCatalog.App
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

            builder.Register(
                    c => HttpContext.Current != null
                        ? new HttpContextWrapper(HttpContext.Current)
                        : c.Resolve<HttpRequestMessage>().Properties["MS_HttpContext"])
                .As<HttpContextBase>()
                .InstancePerRequest();

            builder.Register(c => HttpContext.Current).As<HttpContext>().InstancePerRequest();

            builder.RegisterType<LoggedInUserService>().As<ILoggedInUserService>().InstancePerLifetimeScope();
            builder.RegisterType<MemberService>().As<IMemberService>();
            builder.RegisterType<PublisherApiConfiguration>().As<IPublisherApiConfiguration>()
                .AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<PublishService>().As<IPublishCourseService>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<CourseDbContext>()
                .WithParameter("options", new DbContextOptions<CourseDbContext>())
                .InstancePerLifetimeScope();

            builder
                .RegisterType<IdemContext>()
                .WithParameter("options", new DbContextOptions<IdemContext>())
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IAsyncRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterModule<RepositoryRegistrationModule>();
            builder.RegisterModule<AutoMapperModule>();

            builder.RegisterMediatR(typeof(WebApiApplication).GetTypeInfo().Assembly);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            var connectionString = ConfigurationManager.ConnectionStrings["CourseContext"].ConnectionString;
            var logFile = ConfigurationManager.AppSettings["LogFile"];

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithHttpRequestId()
                .Enrich.WithHttpContextData()
                .Enrich.WithMachineName()
                .Enrich.WithMvcRouteTemplate()
                .Enrich.WithMvcActionName()
                .Enrich.FromLogContext()
                .WriteTo.Logger(new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .Enrich.WithMachineName()
                    .Enrich.FromLogContext()
                    .Enrich.WithHttpRequestId()
                    .Enrich.WithAssemblyName()
                    .Enrich.WithAssemblyVersion()
                    .Enrich.WithHttpContextData()
                    .Enrich.WithClaimValue("AlsdeId")
                    .Enrich.WithUserName()
                    .Enrich.WithHttpRequestRawUrl()
                    .Enrich.WithWebApiActionName("ActionName")
                    .Enrich.WithWebApiControllerName("ControllerName")
                    .Enrich.WithWebApiRouteData("RouteData")
                    .Enrich.WithMvcActionName("ActionName")
                    .Enrich.WithMvcControllerName("ControllerName")
                    .Enrich.WithMvcRouteData("RouteData")
                    .WriteTo
                    .MSSqlServer(connectionString,
                        GetErrorSinkOptions(),
                        columnOptions: GetErrorSqlColumnOptions(),
                        restrictedToMinimumLevel: LogEventLevel.Error)
                    .CreateLogger())
                //.WriteTo.Logger(new LoggerConfiguration()
                //    .MinimumLevel.Verbose()
                //    .Enrich.WithMachineName()
                //    .Enrich.FromLogContext()
                //    .Enrich.WithHttpRequestId()
                //    .Enrich.WithAssemblyName()
                //    .Enrich.WithAssemblyVersion()
                //    .Enrich.WithHttpContextData()
                //    .Enrich.WithUserName()
                //    .Enrich.WithMvcActionName("ActionName")
                //    .Enrich.WithMvcControllerName("ControllerName")
                //    .Enrich.WithMvcRouteData("RouteData")
                //    .WriteTo
                //    .MSSqlServer(connectionString: connectionString,
                //        sinkOptions: GetSinkOptions(),
                //        columnOptions: GetSqlColumnOptions(),
                //        restrictedToMinimumLevel: LogEventLevel.Information)
                //    .CreateLogger())
                //.WriteTo.File(new CompactJsonFormatter(), logFile, LogEventLevel.Error,
                //    shared: true, rollingInterval: RollingInterval.Month
                //)
                .CreateLogger();
        }

        private static MSSqlServerSinkOptions GetErrorSinkOptions()
        {
            var options = new MSSqlServerSinkOptions
            {
                AutoCreateSqlTable = false,
                SchemaName = "Log",
                TableName = "Error"
            };

            return options;
        }

        private static ColumnOptions GetErrorSqlColumnOptions()
        {
            var options = new ColumnOptions { Id = { ColumnName = "LogId" } };

            options.Store.Remove(StandardColumn.MessageTemplate);
            options.Store.Remove(StandardColumn.Level);
            options.Store.Remove(StandardColumn.Properties);


            options.Store.Add(StandardColumn.LogEvent);
            options.LogEvent.ExcludeStandardColumns = true;
            options.LogEvent.ExcludeAdditionalProperties = true;

            options.AdditionalColumns = new List<SqlColumn>
            {
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "Location", PropertyName = "RawUrl"},
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "AlsdeId"},
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "UserName"},
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "Hostname", PropertyName = "MachineName"},
                new SqlColumn
                    {DataType = SqlDbType.VarChar, ColumnName = "CorrelationId", PropertyName = "HttpRequestId"},
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "AssemblyName"},
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "AssemblyVersion"},
                new SqlColumn
                    {DataType = SqlDbType.VarChar, ColumnName = "Controller", PropertyName = "ControllerName"},
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "Action", PropertyName = "ActionName"},
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "RouteData"},
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "HttpContextData"},
                new SqlColumn {DataType = SqlDbType.Float, ColumnName = "ElapsedMilliseconds", AllowNull = false}
            };

            return options;
        }

        private static MSSqlServerSinkOptions GetSinkOptions()
        {
            var options = new MSSqlServerSinkOptions
            {
                AutoCreateSqlTable = false,
                SchemaName = "Log",
                TableName = "PerfNew"
            };

            return options;
        }

        private static ColumnOptions GetSqlColumnOptions()
        {
            var options = new ColumnOptions { Id = { ColumnName = "LogId" } };

            options.Store.Remove(StandardColumn.Message);
            options.Store.Remove(StandardColumn.MessageTemplate);
            options.Store.Remove(StandardColumn.Level);
            options.Store.Remove(StandardColumn.Exception);
            options.Store.Remove(StandardColumn.Properties);

            options.Store.Add(StandardColumn.LogEvent);
            options.LogEvent.ExcludeStandardColumns = true;
            options.LogEvent.ExcludeAdditionalProperties = true;

            options.AdditionalColumns = new List<SqlColumn>
            {
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "Location", PropertyName = "RawUrl"},
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "UserName"},
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "Hostname", PropertyName = "MachineName"},
                new SqlColumn
                    {DataType = SqlDbType.VarChar, ColumnName = "CorrelationId", PropertyName = "HttpRequestId"},
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "AssemblyName"},
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "AssemblyVersion"},
                new SqlColumn
                    {DataType = SqlDbType.VarChar, ColumnName = "Controller", PropertyName = "ControllerName"},
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "Action", PropertyName = "ActionName"},
                new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "RouteData"},
                new SqlColumn {DataType = SqlDbType.Int, ColumnName = "ElapsedMilliseconds", AllowNull = false}
            };

            return options;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            if (ex == null) return;

            Session["Error"] = ex.ToBetterString();

            string errorControllerAction;

            //var logger = new Logger(new LoggingConfiguration());
            MvcWebExtensions.GetHttpStatus(ex, out var httpStatus);
            switch (httpStatus)
            {
                case 404:
                    errorControllerAction = "NotFound";
                    break;
                default:
                    //Alsde.Mvc.Logging.Helpers.LogWebError(Constants.ApplicationName, Constants.LayerName, ex);
                    //logger.LogWebError(Constants.ApplicationName, Constants.LayerName, ex);
                    errorControllerAction = "Index";
                    break;
            }

            var httpContext = ((WebApiApplication)sender).Context;
            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = httpStatus;
            httpContext.Response.TrySkipIisCustomErrors = true;

            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = errorControllerAction;

            var controller = new ErrorController();
            ((IController)controller)
                .Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        }
    }
}