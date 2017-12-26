namespace LearningPlatform.Domain.SurveyPublishing
{
    public class SurveyPublishing
    {

        public delegate SurveyPublishing Factory();
        private readonly PublishingService _publishingService;

        public SurveyPublishing(
            PublishingService publishingService)
        {
            _publishingService = publishingService;
        }

        public void Publish(long surveyId)
        {
            _publishingService.Publish(surveyId);
        }
    }
}
