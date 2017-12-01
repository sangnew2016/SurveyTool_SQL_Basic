using Autofac;
using LearningPlatform.Api.ErrorHandling;
using System.Web.Http.Cors;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using LearningPlatform.Domain.Common;

namespace LearningPlatform.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IContainer container)
        {
            // Web API configuration and services


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SerializerSettings = JsonSerializerSettingsFactory.Create(container.Resolve<IComponentContext>());

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            config.Services.Add(typeof(IExceptionLogger), new GlobalErrorLogger());

            config.EnableCors(new EnableCorsAttribute("*", "*", "GET, POST, OPTIONS, PUT, DELETE"));
        }
    }
}
