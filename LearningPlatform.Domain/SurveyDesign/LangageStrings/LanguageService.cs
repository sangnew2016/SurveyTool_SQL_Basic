using LearningPlatform.Domain.Common;
using System.Collections.Generic;
using System.Globalization;
using Autofac;
using LearningPlatform.Domain.SurveyDesign.Resources;

namespace LearningPlatform.Domain.SurveyDesign.LangageStrings
{
    public class LanguageService
    {
        private readonly IRequestContext _requestContext;
        private readonly IComponentContext _componentContext;

        public LanguageService(IRequestContext requestContext, IComponentContext componentContext)
        {
            _requestContext = requestContext;
            _componentContext = componentContext;
        }

        public string GetString(ILanguageString langString, string language)
        {
            ILanguageStringItem item = langString.GetItem(language);
            if (item == null)
            {
                var defaultSurveyLanguage = "en";
                item = langString.GetItem(defaultSurveyLanguage) ?? langString.FirstItem();
            }
            return item?.Text;
        }

        public void SetString(ILanguageString langString, string language, string value)
        {
            var item = langString.GetItem(language);
            if (item == null)
            {
                langString.AddItem(new LanguageStringItem { Language = language, Text = value });
            }
            else
            {
                item.Text = value;
            }
        }

        public string[] GetExpandedUserLanguages(string[] userLanguages)
        {
            var ret = new List<string>();
            if (userLanguages == null) return ret.ToArray();
            foreach (var lang in userLanguages)
            {
                try
                {
                    if (lang == null) continue;
                    var culture = new CultureInfo(RemoveQualifier(lang), true);
                    while (!string.IsNullOrEmpty(culture.Name))
                    {
                        if (!ret.Contains(culture.Name)) ret.Add(culture.Name);
                        culture = culture.Parent;
                    }
                }
                catch (CultureNotFoundException) {/* If we cannot create a culture, then ignore it.*/  }
            }
            return ret.ToArray();
        }

        private static string RemoveQualifier(string lang)
        {
            if (lang.Contains(";")) return lang.Substring(0, lang.IndexOf(';'));
            return lang;
        }

        public EvaluationString CreateEvaluationString(ILanguageString lang)
        {
            string language = "en";
            var value = GetString(lang, language);
            return CreateEvaluationString(value);
        }

        public EvaluationString CreateEvaluationString(string value)
        {
            var ret = _componentContext.Resolve<EvaluationString>();
            ret.Value = value;
            return ret;
        }
    }
}
