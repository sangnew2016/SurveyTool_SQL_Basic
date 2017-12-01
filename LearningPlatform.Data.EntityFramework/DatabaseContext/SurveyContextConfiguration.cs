using Autofac;
//using System;
//using System.Web.Mvc;
using System.Data.Entity.Migrations;
//using Autofac.Features.ResolveAnything;
//using LearningPlatform.Domain;
//using AutoMapper;
//using System.Linq;
//using LearningPlatform.Data.EntityFramework.DatabaseContext.DemoData;
//using LearningPlatform.Domain.Common;
//using LearningPlatform.Domain.Mapping;

namespace LearningPlatform.Data.EntityFramework.DatabaseContext
{
    /*
    Add-Migration -StartUpProjectName LearningPlatform.Api -ProjectName LearningPlatform.Data.EntityFramework -configuration LearningPlatform.Data.EntityFramework.DatabaseContext.SurveyContextConfiguration __NameOfMigrationStep__
    Update-Database -StartUpProjectName LearningPlatform.Api -ProjectName LearningPlatform.Data.EntityFramework -configuration LearningPlatform.Data.EntityFramework.DatabaseContext.SurveyContextConfiguration
    */
    public sealed class SurveyContextConfiguration: DbMigrationsConfiguration<SurveyContext>
    {
        //private IContainer _tempContainer;

        public SurveyContextConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DatabaseContext\Migrations";
            MigrationsNamespace = "LearningPlatform.Data.EntityFramework.DatabaseContext.Migrations";
            ContextKey = "LearningPlatform.Data.EntityFramework.DatabaseContext.SurveyContextConfiguration";
        }

        //protected override void Seed(SurveyContext context)
        //{
        //    try
        //    {
        //        //... dang ky DI de xai truc tiep khi tao du lieu default

        //        IComponentContext componentContext = DependencyResolver.Current.GetService<IComponentContext>();
        //        if (componentContext == null)
        //        {
        //            // this will execute if you run the Update-Database manually
        //            var builder = new ContainerBuilder();
        //            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
        //            builder.RegisterModule<EntityFrameworkDataModule>();

        //            builder.RegisterModule<DomainModule>();

        //            _tempContainer = builder.Build();
        //            componentContext = _tempContainer;
        //            Mapper.Initialize(config =>
        //            {
        //                config.AddProfile<DomainAutoMapperProfile>();
        //            });
        //        }

        //        if (!context.Surveys.Any())
        //        {
        //            var surveyContextProvider = componentContext.Resolve<DummyRequestObjectProvider<SurveyContext>>();
        //            surveyContextProvider.Set(context);

        //            componentContext.Resolve<SimpleSurveyDefinitionDemo>().InsertData();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        // This is a workaround to see exceptions when running update command from Package Manager Console.
        //        throw new Exception(e.Message);
        //    }
        //    finally
        //    {
        //        _tempContainer?.Dispose();
        //    }

        //}
    }
}
