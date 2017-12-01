using Autofac;
using LearningPlatform.Data.EntityFramework.BaseRepository;
using LearningPlatform.Data.EntityFramework.DatabaseContext;
using LearningPlatform.Data.EntityFramework.Repository;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts;

namespace LearningPlatform.Data.EntityFramework
{
    public class EntityFrameworkDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterContexts(builder);
            RegisterRepositories(builder);
            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>().InstancePerLifetimeScope();
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<SurveyRepository>().As<ISurveyRepository>().InstancePerLifetimeScope();
        }

        private static void RegisterContexts(ContainerBuilder builder)
        {
            builder.RegisterType<SurveyContextProvider>().InstancePerLifetimeScope();
            builder.RegisterType<RequestObjectProvider<SurveyContext>>().As<IRequestObjectProvider<SurveyContext>>().InstancePerLifetimeScope();
        }
    }
}
