using Autofac;
using LearningPlatform.Data.Memory.Repository;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.SurveyPublishing;

namespace LearningPlatform.Data.Memory
{
    public class DataMemoryAccessModule : Module, IDataMemoryAccessModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<ResourceStringMemoryRepository>().As<IResourceStringRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SurveyVersionMemoryRepository>().As<ISurveyVersionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DummyUnitOfWorkFactory>().As<IUnitOfWorkFactory>().InstancePerLifetimeScope();
        }
    }
}
