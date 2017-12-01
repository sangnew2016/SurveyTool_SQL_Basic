using LearningPlatform.Domain.SurveyDesign;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace LearningPlatform.Data.EntityFramework.Mapping.SurveyDesigner
{
    internal class SurveyMap : EntityTypeConfiguration<Survey>
    {
        public SurveyMap()
        {
            Property(survey => survey.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));


            //thiet lap RowVersion day (C# ho tro full)
            Property(t => t.RowVersion).IsRowVersion();
        }
    }
}
