using LearningPlatform.Domain.Constants;
using LearningPlatform.Domain.Exceptions;
using LearningPlatform.Domain.SurveyDesign;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts;

namespace LearningPlatform.Domain.SurveyPublishing
{
    public class SurveyPublishing
    {

        public delegate SurveyPublishing Factory();
        private readonly SurveyDefinitionService _surveyDefinitionService;
        private readonly PublishingService _publishingService;
        private readonly ISurveyVersionRepository _surveyVersionRepository;

        public SurveyPublishing(
            SurveyDefinitionService surveyDefinitionService,
            PublishingService publishingService,
            ISurveyVersionRepository surveyVersionRepository)
        {
            _surveyDefinitionService = surveyDefinitionService;
            _publishingService = publishingService;
            _surveyVersionRepository = surveyVersionRepository;
        }

        public Survey Publish(long surveyId)
        {
            var survey = _surveyDefinitionService.GetSurveyInfoById(surveyId);
            if (survey.IsSurveyClosed)
            {
                throw new SurveyClosedException("Cannot publish a closed survey.");
            }

            UpdateOpenStatusForSurvey(survey);
            _publishingService.Publish(surveyId);
            return survey;
        }

        private void UpdateOpenStatusForSurvey(Survey survey)
        {
            var surveyVersions = _surveyVersionRepository.GetAll(survey.Id);
            if (surveyVersions == null || surveyVersions.Count < 1)
            {
                _surveyDefinitionService.UpdateSurveyStatus(survey, SurveyStatus.Open);
            }
        }
    }
}
