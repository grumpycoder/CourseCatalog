using Autofac;
using AutoMapper;
using CourseCatalog.App.Profiles;

namespace CourseCatalog.App
{
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