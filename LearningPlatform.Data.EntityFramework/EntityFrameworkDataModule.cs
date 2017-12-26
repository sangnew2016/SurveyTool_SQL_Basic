using Autofac;
using LearningPlatform.Data.EntityFramework.BaseRepository;
using LearningPlatform.Data.EntityFramework.DatabaseContext;
using LearningPlatform.Data.EntityFramework.Repository;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts;
using LearningPlatform.Domain.SurveyPublishing;

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
            builder.RegisterType<SurveyVersionRepository>().As<ISurveyVersionRepository>().InstancePerLifetimeScope();
        }

        private static void RegisterContexts(ContainerBuilder builder)
        {
            //Note: ve ban chat SurveyContextProvider & RequestObjectProvider<SurveyContext> la nhu nhau
            // -> khi ta dang ky the nay, se tao 2 context khac nhau
            // -> SaveChanges khong affect (careful cho cai nay)

            //builder.RegisterType<SurveyContextProvider>().InstancePerLifetimeScope();
            builder.RegisterType<RequestObjectProvider<SurveyContext>>().As<IRequestObjectProvider<SurveyContext>>().SingleInstance();
        }
    }
}
