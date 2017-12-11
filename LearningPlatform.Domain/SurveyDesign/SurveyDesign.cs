using LearningPlatform.Domain.Constants;
using LearningPlatform.Domain.SurveyDesign.LangageStrings;
using System;
using System.Globalization;

namespace LearningPlatform.Domain.SurveyDesign
{
    public class SurveyDesign
    {
        //Mot Delegate de ben file hkac xai ma khong
        public delegate SurveyDesign Factory(long? surveyId = null, bool useDatabaseIds = false, string language = MultipleLanguages.DEFAULT_LANGUAGE);

        private bool _useDatabaseIds;
        private string _language;
        private readonly Survey _survey;
        private readonly LanguageStringFactory _languageStringFactory;
        private readonly LanguageService _languageService;

        public SurveyDesign(long? surveyId, bool useDatabaseIds, string language,
            LanguageStringFactory languageStringFactory,
            LanguageService languageService)
        {
            _survey = new Survey()
            {
                Status = SurveyStatus.New,
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            if (surveyId.HasValue) _survey.Id = surveyId.Value;

            _useDatabaseIds = useDatabaseIds;
            _language = language;
            _languageStringFactory = languageStringFactory;
            _languageService = languageService;
        }

        public long SurveyId => _survey.Id;

        public Survey Survey(string surveyModelName, string userId, string[] title, string[] description)
        {
            _survey.Name = surveyModelName;
            _survey.UserId = userId;
            _survey.Title = CreateLanguageString(title);
            _survey.Description = CreateLanguageString(description);

            return _survey;
        }

        private string[] ToArray(string str)
        {
            if (str == null) return null;
            return new[] { str };
        }

        public LanguageString CreateLanguageString(string[] strings)
        {
            if (strings == null) return null;
            var languageString = _languageStringFactory.Create();
            foreach (var s in strings)
            {
                string lang = _language;
                var str = s;
                if (s.Contains("::"))
                {
                    var index = s.IndexOf("::", StringComparison.Ordinal);
                    var specifiedLang = s.Substring(0, index);
                    try
                    {
                        var culture = new CultureInfo(specifiedLang, true);
                        lang = culture.Name;
                        str = s.Substring(index + 2);
                    }
                    catch (CultureNotFoundException) {/* If we cannot create a culture, then ignore it.*/  }
                }
                _languageService.SetString(languageString, lang, str);
            }
            languageString.SurveyId = SurveyId;
            return languageString;
        }

        public LanguageString CreateLanguageString(string value)
        {
            return CreateLanguageString(ToArray(value));
        }
    }
}
