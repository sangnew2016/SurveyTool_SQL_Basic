using LearningPlatform.Data.EntityFramework.Mapping.SurveyDesigner;
using LearningPlatform.Domain.SurveyDesign;
using System.Data.Entity;
using System.Data.Entity.Validation;
using LearningPlatform.Domain.SurveyDesign.LangageStrings;
using LearningPlatform.Domain.SurveyDesign.Resources;

namespace LearningPlatform.Data.EntityFramework.DatabaseContext
{
    public class SurveyContext: DbContext
    {
        static SurveyContext()
        {
            Database.SetInitializer(new SurveyContextInitializer());
        }

        public SurveyContext(): base("Name=SurveysContext")
        {
        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<LanguageString> LanguageStrings { get; set; }
        public DbSet<ResourceString> ResourceStrings { get; set; }
        public DbSet<ResourceStringItem> ResourceStringItems { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SurveyMap());

            modelBuilder.Configurations.Add(new LanguageStringMap());
            modelBuilder.Configurations.Add(new LanguageStringItemMap());

            modelBuilder.Configurations.Add(new ResourceStringMap());
            modelBuilder.Configurations.Add(new ResourceStringItemMap());
        }

        public override int SaveChanges()
        {
            try
            {
                var ret = base.SaveChanges();

                return ret;
            }
            catch (DbEntityValidationException exception)
            {
                foreach (var errorEntityValidation in exception.EntityValidationErrors)
                {
                    var msg =
                        $"Entity of type \"{errorEntityValidation.Entry.Entity.GetType().Name}\" in state \"{errorEntityValidation.Entry.State}\" has the following validation errors:";
                    foreach (var validationError in errorEntityValidation.ValidationErrors)
                    {
                        msg +=
                            $"---- Property: \"{validationError.PropertyName}\", Error: \"{validationError.ErrorMessage}\"";
                    }
                }
                throw;
            }
        }
    }
}
