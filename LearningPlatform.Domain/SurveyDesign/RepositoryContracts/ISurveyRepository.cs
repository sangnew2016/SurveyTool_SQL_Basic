using LearningPlatform.Domain.SurveyDesign.RepositoryContracts.Models;
using System.Collections.Generic;

namespace LearningPlatform.Domain.SurveyDesign.RepositoryContracts
{
    public interface ISurveyRepository
    {
        void Add(Survey survey);
        void Update(Survey survey);
        Survey GetById(long surveyId);
        Survey GetSurveyInfoById(long surveyId);
        Survey GetWithoutAllIncludings(long surveyId);
        Survey GetWithAllRowVersionsOfPages(long surveyId);
        List<Survey> GetAllSurveys();
        IEnumerable<Survey> GetByUserId(string userId);
        IEnumerable<Survey> SearchSurveys(SurveySearchFilter surveySearchModel, string userId);
        bool Exists(long surveyId);
    }
}
