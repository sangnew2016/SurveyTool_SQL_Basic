using LearningPlatform.Domain.Common.Utils;
using LearningPlatform.Domain.Constants;
using LearningPlatform.Domain.Exceptions;

namespace LearningPlatform.Domain.SurveyDesign.RepositoryContracts
{
    public class SurveyDefinitionService
    {
        private readonly ISurveyRepository _surveyRepository;

        public SurveyDefinitionService(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public void UpdateSurveyStatus(Survey surveyInfo, SurveyStatus status)
        {
            surveyInfo.Status = status;
            surveyInfo.RowVersion = GuidUtil.GenerateGuidAsByteArray();
            _surveyRepository.Update(surveyInfo);
        }

        public Survey GetSurveyInfoById(long surveyId)
        {
            Survey survey = _surveyRepository.GetSurveyInfoById(surveyId);
            if (survey == null) throw new SurveyNotFoundException($"Survey with id = {surveyId} not found");
            return survey;
        }


    }
}
