using LearningPlatform.Domain.Common;
using System.Diagnostics;

namespace LearningPlatform.Data.EntityFramework.DatabaseContext
{
    public class SurveyContextProvider
    {
        private readonly IRequestObjectProvider<SurveyContext> _requestSurveyContext;

        public SurveyContextProvider(IRequestObjectProvider<SurveyContext> requestSurveyContext)
        {
            _requestSurveyContext = requestSurveyContext;
        }


        public SurveyContext Get()
        {
            var context = _requestSurveyContext.Get();
            if (context == null)
            {
                context = new SurveyContext();
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                context.Database.Log = Logger;
                _requestSurveyContext.Set(context);
            }
            return context;
        }

        private static void Logger(string obj)
        {
            Debug.WriteLine(obj);
        }
    }
}
