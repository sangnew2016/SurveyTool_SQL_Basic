using LearningPlatform.Domain.SurveyDesign.Questions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Data.EntityFramework.Mapping.SurveyDesigner.Questions
{
    internal class QuestionDefinitionMap : EntityTypeConfiguration<QuestionDefinition>
    {
        public QuestionDefinitionMap()
        {
            Property(t => t.SurveyId)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));

            Property(t => t.Alias)
                .IsRequired()
                .IsVariableLength()
                .HasMaxLength(50);

            Property(t => t.RowVersion).IsRowVersion();

            ToTable("Questions");
        }
    }
}
