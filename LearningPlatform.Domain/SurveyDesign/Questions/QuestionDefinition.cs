using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.SurveyDesign.Pages;
using Newtonsoft.Json;

namespace LearningPlatform.Domain.SurveyDesign.Questions
{
    public abstract class QuestionDefinition : IVersionable
    {

        public long Id { get; set; }
        public long? PageDefinitionId { get; set; }
        public string Alias { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long SurveyId { get; set; }

        public int Position { get; set; }

        [JsonIgnore]
        public PageDefinition PageDefinition { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
