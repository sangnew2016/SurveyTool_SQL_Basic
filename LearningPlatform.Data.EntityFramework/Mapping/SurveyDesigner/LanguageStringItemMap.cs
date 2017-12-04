using LearningPlatform.Domain.SurveyDesign.LangageStrings;
using System.Data.Entity.ModelConfiguration;

namespace LearningPlatform.Data.EntityFramework.Mapping.SurveyDesigner
{
    internal class LanguageStringItemMap : EntityTypeConfiguration<LanguageStringItem>
    {
        public LanguageStringItemMap()
        {
            Property(t => t.Text)
                .IsRequired();

            ToTable("LanguageStringItems");
        }
    }
}
