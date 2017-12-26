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

        public void Publish(long surveyId)
        {
            var publisher = _surveyPublishingFactory.Invoke();
            publisher.Publish(surveyId);
        }

    }
}
