using Autofac;
using LearningPlatform.Application.SurveyDesign;

namespace LearningPlatform.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SurveyDefinitionAppService>().InstancePerLifetimeScope();
        }
    }
}
