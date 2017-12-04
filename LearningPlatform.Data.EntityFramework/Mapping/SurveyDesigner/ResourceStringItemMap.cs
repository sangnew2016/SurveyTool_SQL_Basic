using System.Data.Entity.ModelConfiguration;
using LearningPlatform.Domain.SurveyDesign.Resources;

namespace LearningPlatform.Data.EntityFramework.Mapping.SurveyDesigner
{
    internal class ResourceStringItemMap : EntityTypeConfiguration<ResourceStringItem>
    {
        public ResourceStringItemMap()
        {
            Property(t => t.Text).IsRequired();

            // Table & Column Mappings
            ToTable("ResourceItems");
        }
    }
}
