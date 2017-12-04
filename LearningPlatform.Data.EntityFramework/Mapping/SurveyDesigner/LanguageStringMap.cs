using LearningPlatform.Domain.SurveyDesign.LangageStrings;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace LearningPlatform.Data.EntityFramework.Mapping.SurveyDesigner
{
    internal class LanguageStringMap : EntityTypeConfiguration<LanguageString>
    {
        public LanguageStringMap()
        {
            Property(t => t.SurveyId)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));

            HasMany(k => k.Items)
                .WithRequired()
                .HasForeignKey(k => k.LanguageStringId);

            ToTable("LanguageStrings");
        }
    }
}
