using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using CourseCatalog.App.Profiles;
using CourseCatalog.App.Services;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Persistence;
using CourseCatalog.Persistence.Repositories;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.ObjectModel;
using System.Data;
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
                    c => HttpContext.Current != null ?
                        new HttpContextWrapper(HttpContext.Current) :
                        c.Resolve<System.Net.Http.HttpRequestMessage>().Properties["MS_HttpContext"])
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


            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Information)
                .Enrich.WithMachineName()
                .Enrich.FromLogContext()
                .Enrich.WithAssemblyName()
                .Enrich.WithAssemblyVersion()
                //.WriteTo.Logger(lc => lc
                //        //.Filter.ByIncludingOnly(Matching.WithProperty("ElapsedMilliseconds"))
                //        .WriteTo.MSSqlServer(connectionString: @"Server=.;Database=Courses;Trusted_Connection=True;", 
                //                                sinkOptions: GetSinkOptions(), 
                //                                columnOptions: GetSqlColumnOptions())
                //    )
                //.WriteTo.MSSqlServer(connectionString: @"Server=.;Database=Courses;Trusted_Connection=True;",
                //        sinkOptions: GetSinkOptions(),
                //        columnOptions: GetSqlColumnOptions()
                //        )
                .WriteTo.File(new CompactJsonFormatter(), @"\\alsdepfs\WebDev\Test\Logs\courses\courses-test-flatfile.json", rollingInterval: RollingInterval.Minute)
                .CreateLogger();

        }

        private static MSSqlServerSinkOptions GetSinkOptions()
        {
            var options = new MSSqlServerSinkOptions
            {
                AutoCreateSqlTable = true,
                SchemaName = "Log",
                TableName = "ErrorNew"
            };

            return options;
        }

        private static ColumnOptions GetSqlColumnOptions()
        {
            var options = new ColumnOptions();
            //options.Store.Remove(StandardColumn.Message);
            options.Store.Remove(StandardColumn.MessageTemplate);
            //options.Store.Remove(StandardColumn.Level);
            options.Store.Remove(StandardColumn.Exception);

            options.Store.Remove(StandardColumn.Properties);
            //options.Store.Add(StandardColumn.LogEvent);
            options.LogEvent.ExcludeStandardColumns = true;
            options.LogEvent.ExcludeAdditionalProperties = true;

            options.AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn
                { ColumnName = "PerfItem", AllowNull = false,
                    DataType = SqlDbType.NVarChar, DataLength = 100,
                    NonClusteredIndex = true },
                new SqlColumn
                {
                    ColumnName = "ElapsedMilliseconds", AllowNull = false,
                    DataType = SqlDbType.Int
                },
                new SqlColumn
                {
                    ColumnName = "ActionName", AllowNull = false
                },
                new SqlColumn
                {
                    ColumnName = "MachineName", AllowNull = false
                }
            };

            return options;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //var ex = Server.GetLastError();
            //if (ex == null) return;

            ////Session["Error"] = ex.ToBetterString();

            //string errorControllerAction;

            //var logger = new Logger(new LoggingConfiguration());
            //MvcWebExtensions.GetHttpStatus(ex, out var httpStatus);
            //switch (httpStatus)
            //{
            //    case 404:
            //        errorControllerAction = "NotFound";
            //        break;
            //    default:
            //        //Alsde.Mvc.Logging.Helpers.LogWebError(Constants.ApplicationName, Constants.LayerName, ex);
            //        logger.LogWebError(Constants.ApplicationName, Constants.LayerName, ex);
            //        errorControllerAction = "Index";
            //        break;
            //}

            //var httpContext = ((WebApiApplication)sender).Context;
            //httpContext.ClearError();
            //httpContext.Response.Clear();
            //httpContext.Response.StatusCode = httpStatus;
            //httpContext.Response.TrySkipIisCustomErrors = true;

            //var routeData = new RouteData();
            //routeData.Values["controller"] = "Error";
            //routeData.Values["action"] = errorControllerAction;

            //var controller = new ErrorController();
            //((IController)controller)
            //    .Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        }
    }

    public class RepositoryRegistrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerRequest();
        }
    }

    public class AutoMapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Also register any custom type converter/value resolvers
            //builder.RegisterType<CustomValueResolver>().AsSelf();
            //builder.RegisterType<CustomTypeConverter>().AsSelf();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mappings>();
            })).AsSelf().SingleInstance();

            builder.Register(c =>
                {
                    //This resolves a new context that can be used later.
                    var context = c.Resolve<IComponentContext>();
                    var config = context.Resolve<MapperConfiguration>();
                    return config.CreateMapper(context.Resolve);
                })
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }

}

