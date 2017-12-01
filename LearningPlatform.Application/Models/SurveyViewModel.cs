using LearningPlatform.Domain.Constants;

namespace LearningPlatform.Application.Models
{
    public class SurveyViewModel
    {
        public long SurveyId { get; set; }
        public string Name { get; set; }
        public SurveyStatus SurveyStatus { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
