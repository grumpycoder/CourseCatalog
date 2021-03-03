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
using System;
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

            //builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.Register(c => HttpContext.Current).As<HttpContext>().InstancePerRequest();

            builder.RegisterType<LoggedInUserService>().As<ILoggedInUserService>();
            builder.RegisterType<MemberService>().As<IMemberService>();

            builder
                .RegisterType<CourseDbContext>()
                .WithParameter("options", new DbContextOptions<CourseDbContext>())
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
