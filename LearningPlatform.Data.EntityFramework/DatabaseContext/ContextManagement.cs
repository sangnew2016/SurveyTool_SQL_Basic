using System.Data.Entity;

namespace LearningPlatform.Data.EntityFramework.DatabaseContext
{
    public class ContextManagement
    {
        private readonly SurveyContextProvider _surveyContextProvider;
        //private readonly ResponsesContextProvider _responsesContextProvider;

        public ContextManagement(SurveyContextProvider surveyContextProvider)
        {
            _surveyContextProvider = surveyContextProvider;
            //_responsesContextProvider = responsesContextProvider;
        }

        public bool HasChanges
        {
            get
            {
                return _surveyContextProvider.Get().ChangeTracker.HasChanges();
                       //_responsesContextProvider.Get(true).ChangeTracker.HasChanges() ||
                       //_responsesContextProvider.Get(false).ChangeTracker.HasChanges();
            }
        }

        public int SaveChanges()
        {
            int count = _surveyContextProvider.Get().SaveChanges();
            //count += _responsesContextProvider.Get(false).SaveChanges();
            //count += _responsesContextProvider.Get(true).SaveChanges();
            return count;
        }

        public DbContextTransaction BeginTransaction()
        {
            return _surveyContextProvider.Get().Database.BeginTransaction();
        }
    }
}
