using LearningPlatform.Domain.SurveyPublishing;
using System.Collections.Generic;

namespace LearningPlatform.Data.Memory.Repository
{
    public class SurveyVersionMemoryRepository : ISurveyVersionRepository
    {
        private readonly SurveyMemoryContext _context;
        private readonly Dictionary<long, SurveyVersion> _versions = new Dictionary<long, SurveyVersion>();

        public SurveyVersionMemoryRepository(SurveyMemoryContext context)
        {
            _context = context;
        }

        public void Add(SurveyVersion surveyVersion)
        {
            _versions[surveyVersion.SurveyId] = surveyVersion;
            _context.Add(surveyVersion);
        }

        public SurveyVersion GetLatest(long surveyId)
        {
            return _versions[surveyId];
        }

        public List<SurveyVersion> GetAll(long surveyId)
        {
            return null;
        }
    }
}
