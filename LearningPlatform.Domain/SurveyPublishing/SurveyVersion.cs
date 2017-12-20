namespace LearningPlatform.Domain.SurveyPublishing
{
    public class SurveyVersion
    {
        public long Id { get; set; }
        public long SurveyId { get; set; }
        public string SerializedString { get; set; }
    }
}
