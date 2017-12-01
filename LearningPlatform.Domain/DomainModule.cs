using Autofac;
using LearningPlatform.Domain.Common;

namespace LearningPlatform.Domain
{
    public class DomainModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestObjectProvider<IRequestContext>>().As<IRequestObjectProvider<IRequestContext>>().InstancePerLifetimeScope();
        }
    }
}
