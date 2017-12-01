using LearningPlatform.Data.EntityFramework.Mapping.SurveyDesigner;
using LearningPlatform.Domain.SurveyDesign;
using System.Data.Entity;
using System.Data.Entity.Validation;

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


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SurveyMap());
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
                    var msg = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        errorEntityValidation.Entry.Entity.GetType().Name, errorEntityValidation.Entry.State);
                    foreach (var validationError in errorEntityValidation.ValidationErrors)
                    {
                        msg += string.Format("---- Property: \"{0}\", Error: \"{1}\"", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
