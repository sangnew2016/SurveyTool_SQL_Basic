using Autofac;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.SurveyDesign.Scripting;
using LearningPlatform.Domain.SurveyExecution.Request;

namespace LearningPlatform.Domain
{
    public class DomainModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestObjectProvider<IRequestContext>>().As<IRequestObjectProvider<IRequestContext>>().InstancePerLifetimeScope();
            builder.RegisterType<RequestContextWrapper>().As<IRequestContext>().InstancePerLifetimeScope();
            builder.RegisterType<ScriptExecutor>().As<IScriptExecutor>().InstancePerLifetimeScope();
            builder.RegisterType<HostObject>().As<IHostObject>().InstancePerLifetimeScope();
            builder.RegisterType<ScriptCodeReader>().As<IScriptCodeReader>().InstancePerLifetimeScope();
        }
    }
}
