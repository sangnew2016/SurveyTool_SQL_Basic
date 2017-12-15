using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningPlatform.Domain.SurveyDesign.Pages;

namespace LearningPlatform.Data.EntityFramework.Mapping.SurveyDesigner.Nodes
{
    internal class PageDefinitionMap : EntityTypeConfiguration<PageDefinition>
    {
        public PageDefinitionMap()
        {
            Property(p => p.Seed)
                .HasColumnName("Seed"); // Need this to make sure the column reuses the Seed column from Folder.

            Property(p => p.ResponseStatus)
                .IsOptional()
                .IsVariableLength()
                .HasMaxLength(30);

            HasMany(k => k.ChildNodes);

            ToTable("Nodes");
        }
    }
}
