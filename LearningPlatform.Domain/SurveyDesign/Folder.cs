using System.Collections.Generic;
using Newtonsoft.Json;

namespace LearningPlatform.Domain.SurveyDesign
{
    public class Folder : Node
    {
        public Folder(string alias)
        {
            Alias = alias;
        }

        protected Folder()
        { }

        public int Seed { get; set; }


        [JsonIgnore]
        public override IList<string> QuestionAliases
        {
            get
            {
                var list = new List<string>();
                foreach (Node folderNode in ChildNodes)
                    list.AddRange(folderNode.QuestionAliases);
                return list;
            }
        }

    }
}
