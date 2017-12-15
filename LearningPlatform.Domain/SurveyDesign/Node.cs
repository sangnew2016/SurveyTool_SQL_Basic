using LearningPlatform.Domain.Common;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LearningPlatform.Domain.SurveyDesign
{
    public abstract class Node : IVersionable
    {
        protected Node()
        {
            ChildNodes = new List<Node>();
        }

        public long Id { get; set; }
        public string Alias { get; set; }

        public long? ParentId { get; set; }
        public long SurveyId { get; set; }

        public IList<Node> ChildNodes { get; set; }

        [JsonIgnore]
        public Node Parent { get; set; }

        public abstract IList<string> QuestionAliases { get; }
        public string NodeType { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
