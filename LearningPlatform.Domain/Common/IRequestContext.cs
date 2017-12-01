using LearningPlatform.Domain.SurveyDesign;
using Newtonsoft.Json.Linq;

namespace LearningPlatform.Domain.Common
{
    public interface IRequestContext
    {
        Survey Survey { get; }
        JObject CustomColumns { get; set; }
        bool IsForward { get; }
        string[] UserLanguages { get; }
    }
}
