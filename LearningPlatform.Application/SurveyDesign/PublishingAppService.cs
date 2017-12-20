using LearningPlatform.Domain.SurveyDesign;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts;
using LearningPlatform.Domain.SurveyPublishing;
using LearningPlatform.Domain.Constants;
using LearningPlatform.Domain.Exceptions;

namespace LearningPlatform.Application.SurveyDesign
{
    public class PublishingAppService
    {
        private readonly SurveyPublishing.Factory _surveyPublishingFactory;

        public PublishingAppService(SurveyPublishing.Factory surveyPublishingFactory)
        {
            _surveyPublishingFactory = surveyPublishingFactory;
        }

        public Survey Publish(long surveyId)
        {
            var publisher = _surveyPublishingFactory.Invoke();
            var survey = publisher.Publish(surveyId);
            return survey;
        }

    }
}
