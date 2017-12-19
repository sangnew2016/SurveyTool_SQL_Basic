using LearningPlatform.Domain.SurveyDesign;
using System.Data.Entity.ModelConfiguration;

namespace LearningPlatform.Data.EntityFramework.Mapping.SurveyDesigner.Nodes
{
    internal class FolderMap : EntityTypeConfiguration<Folder>
    {
        public FolderMap()
        {
            Property(p => p.Seed)
                .HasColumnName("Seed"); // Need this to make sure the column reuses the Seed column from PageDefinition.

            ToTable("Nodes");
        }
    }
}
