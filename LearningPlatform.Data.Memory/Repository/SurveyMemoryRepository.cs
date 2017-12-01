using LearningPlatform.Domain.SurveyDesign;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts.Models;
using System.Collections.Generic;

namespace LearningPlatform.Data.Memory.Repository
{
    public class SurveyMemoryRepository : ISurveyRepository
    {
        private readonly SurveyMemoryContext _context;
        private readonly Dictionary<long, Survey> _surveys = new Dictionary<long, Survey>();

        public SurveyMemoryRepository(SurveyMemoryContext context)
        {
            _context = context;
        }

        public void Clear()
        {
            _context.Clear();
            _surveys.Clear();
        }

        public void Add(Survey survey)
        {
            survey.Id = _surveys.Count + 1;
            _surveys[survey.Id] = survey;
            _context.Add(survey);
        }

        public Survey GetById(long surveyId)
        {
            return _surveys[surveyId];
        }

        public Survey GetWithSurveySettingsAndTopFolderById(long surveyId)
        {
            return GetById(surveyId);
        }

        public Survey GetSurveyInfoById(long surveyId)
        {
            return GetById(surveyId);
        }

        public void Update(Survey survey)
        {
            _context.Add(survey);
        }

        public void UpdateModifiedDate(long surveyId)
        {

        }

        public void UpdateLastPublished(long surveyId)
        {

        }

        public List<Survey> GetAllSurveys()
        {
            return null;
        }
        public List<Survey> GetAllSurveys(int start, int limit)
        {
            return null;
        }

        public IEnumerable<Survey> GetByUserId(string userId)
        {
            return null;
        }

        public Survey GetWithoutAllIncludings(long surveyId)
        {
            return _surveys[surveyId];
        }

        public Survey GetWithAllRowVersionsOfPages(long surveyId)
        {
            return null;
        }

        public IEnumerable<Survey> SearchSurveys(SurveySearchFilter searchModel, string userId)
        {
            return null;
        }

        public bool Exists(long surveyId)
        {
            return _surveys.ContainsKey(surveyId);
        }
    }
}
