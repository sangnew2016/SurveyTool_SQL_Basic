using Autofac;
using Autofac.Integration.WebApi;
using LearningPlatform.Api.ActionFilters;
using LearningPlatform.Api.App_Start;
using Owin;
using System.Reflection;
using System.Web.Http;

namespace LearningPlatform.Api
{
    public class Startup
    {
        private IContainer _container;

        public void Configuration(IAppBuilder app)
        {
            var builder = AutofacConfig.Configure();
            _container = builder.Build();
            AutoMapperConfig.Configure();

            //log
            log4net.Config.XmlConfigurator.Configure();

            //cors
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //DI
            var config = new HttpConfiguration();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            config.DependencyResolver = new SingleScopeDependencyResolver(_container);
            config.Filters.Add(_container.Resolve<SaveChangesActionFilter>());

            //swagger
            SwaggerConfig.Register(config);

            WebApiConfig.Register(config, _container);
            app.UseWebApi(config);
        }
    }
}