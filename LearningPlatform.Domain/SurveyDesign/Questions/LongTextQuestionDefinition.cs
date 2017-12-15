namespace LearningPlatform.Domain.SurveyDesign.Questions
{
    public class LongTextQuestionDefinition: OpenEndedTextQuestionDefinition
    {
        public int? Cols { get; set; }
        public int? Rows { get; set; }
    }
}
