using Autofac;
using LearningPlatform.Data.Memory.Repository;
using LearningPlatform.Domain.Common;

namespace LearningPlatform.Data.Memory
{
    public class DataMemoryAccessModule : Module, IDataMemoryAccessModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<ResourceStringMemoryRepository>().As<IResourceStringRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DummyUnitOfWorkFactory>().As<IUnitOfWorkFactory>().InstancePerLifetimeScope();
        }
    }
}
