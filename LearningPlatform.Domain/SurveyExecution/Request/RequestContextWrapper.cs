using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.SurveyDesign;
using Newtonsoft.Json.Linq;

namespace LearningPlatform.Domain.SurveyExecution.Request
{
    public class RequestContextWrapper : IRequestContext
    {
        private readonly IRequestObjectProvider<IRequestContext> _requestContextProvider;

        public RequestContextWrapper(IRequestObjectProvider<IRequestContext> requestContextProvider)
        {
            _requestContextProvider = requestContextProvider;
        }

        public Survey Survey
        {
            get { return _requestContextProvider.Get().Survey; }
        }

        public JObject CustomColumns { get; set; }


        public bool IsForward
        {
            get { return _requestContextProvider.Get().IsForward; }
        }

        public string[] UserLanguages
        {
            get { return _requestContextProvider.Get().UserLanguages; }
        }

    }
}
