using System;
using Autofac;

namespace LearningPlatform.Domain.SurveyDesign.LangageStrings
{
    public class LanguageStringFactory
    {
        private readonly IComponentContext _componentContext;

        public LanguageStringFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public LanguageString Create(Action<LanguageString> settings = null)
        {
            var languageString = _componentContext.Resolve<LanguageString>();
            //if (settings != null) settings(languageString);
            settings?.Invoke(languageString);
            return languageString;
        }
    }
}
