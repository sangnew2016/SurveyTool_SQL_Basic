namespace LearningPlatform.Domain.SurveyDesign.LangageStrings
{
    public interface ILanguageString
    {
        ILanguageStringItem GetItem(string language);
        void AddItem(ILanguageStringItem languageStringItem);
        ILanguageStringItem FirstItem();
    }
}
