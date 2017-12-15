using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LearningPlatform.Domain.SurveyDesign.Questions;
using Newtonsoft.Json;

namespace LearningPlatform.Domain.SurveyDesign.Pages
{
    public class PageDefinition : Node
    {
        public PageDefinition()
        {
            QuestionDefinitions = new Collection<QuestionDefinition>();
        }

        public PageDefinition(params QuestionDefinition[] questionDefinitions) : this()
        {
            if (questionDefinitions != null)
            {
                foreach (var node in questionDefinitions)
                {
                    QuestionDefinitions.Add(node);
                }
            }
        }


        public ICollection<QuestionDefinition> QuestionDefinitions { get; private set; }

        public int Seed { get; set; }
        public int Position { get; set; }
        public string ResponseStatus { get; set; }

        [JsonIgnore]
        public override IList<string> QuestionAliases
        {
            get { return QuestionDefinitions.Select(questionDefinition => questionDefinition.Alias).ToList(); }
        }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
