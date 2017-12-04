namespace LearningPlatform.Domain.SurveyDesign.LangageStrings
{
    public class LanguageStringItem : ILanguageStringItem
    {
        public long Id { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }
        public long LanguageStringId { get; set; }
    }
}
