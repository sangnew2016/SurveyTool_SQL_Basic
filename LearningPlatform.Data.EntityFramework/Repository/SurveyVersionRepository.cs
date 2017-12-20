using LearningPlatform.Data.EntityFramework.BaseRepository;
using LearningPlatform.Data.EntityFramework.DatabaseContext;
using LearningPlatform.Domain.SurveyPublishing;
using System.Collections.Generic;
using System.Linq;

namespace LearningPlatform.Data.EntityFramework.Repository
{
    internal class SurveyVersionRepository : ISurveyVersionRepository
    {
        private readonly GenericRepository<SurveyVersion> _genericRepository;

        public SurveyVersionRepository(GenericRepository<SurveyVersion> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        private SurveyContext Context { get { return _genericRepository.Context; } }

        public void Add(SurveyVersion surveyVersion)
        {
            _genericRepository.Add(surveyVersion);
        }

        public SurveyVersion GetLatest(long surveyId)
        {
            return Context.SurveyVersions.Where(v => v.SurveyId == surveyId).OrderByDescending(p => p.Id).FirstOrDefault();
        }

        public List<SurveyVersion> GetAll(long surveyId)
        {
            return Context.SurveyVersions.Where(v => v.SurveyId == surveyId).ToList();
        }
    }
}
