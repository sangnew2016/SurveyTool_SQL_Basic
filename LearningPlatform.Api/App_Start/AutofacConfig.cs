using Autofac;
using Autofac.Features.ResolveAnything;
using LearningPlatform.Application;
using LearningPlatform.Data.EntityFramework;
using LearningPlatform.Domain;

namespace LearningPlatform.Api.App_Start
{
    public static class AutofacConfig
    {
        public static ContainerBuilder Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<EntityFrameworkDataModule>();

            //builder.RegisterModule<ElasticsearchDataModule>();
            //builder.RegisterModule<SurveyExecutionDataModule>();
            //builder.RegisterType<DataMemoryAccessModule>().As<IDataMemoryAccessModule>().InstancePerLifetimeScope();

            return builder;
        }
    }
}