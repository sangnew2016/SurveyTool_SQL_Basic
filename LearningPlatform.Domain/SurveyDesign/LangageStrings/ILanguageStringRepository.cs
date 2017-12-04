using System.Collections.Generic;

namespace LearningPlatform.Domain.SurveyDesign.LangageStrings
{
    public interface ILanguageStringRepository
    {
        void Add(LanguageString languageStringItem);
        void Update(LanguageString languageStringItem);
        void Delete(long id);
        void Delete(IEnumerable<LanguageString> languages);
    }
}
