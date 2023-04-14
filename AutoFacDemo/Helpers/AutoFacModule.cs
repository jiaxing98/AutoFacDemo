using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoFacDemo.Data;
using AutoFacDemo.Repositories;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace AutoFacDemo.Helpers
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // automapper
            builder.Register(c =>
            {
                IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
                return mapper;
            })
            .As<IMapper>()
            .SingleInstance();

            // mongodb settings
            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var sp = context.Resolve<IServiceProvider>();
                return sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
            })
            .As<IMongoDBSettings>()
            .SingleInstance();

            // Other Lifetime
            // Transient
            //builder.RegisterType<MyService>().As<IService>()
            //    .InstancePerDependency();

            // Scoped
            builder.RegisterType<BookRepository>().As<IBookRepository>()
                .InstancePerLifetimeScope();

            //builder.RegisterType<BookRepository>().As<IBookRepository>()
            //    .InstancePerRequest();

            // Singleton
            builder.RegisterType<MongoDBContext>().As<IMongoDBContext>()
                    .SingleInstance();

            // Scan an assembly for components
            //builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
            //       .Where(t => t.Name.EndsWith("Service"))
            //       .AsImplementedInterfaces();
        }
    }
}
