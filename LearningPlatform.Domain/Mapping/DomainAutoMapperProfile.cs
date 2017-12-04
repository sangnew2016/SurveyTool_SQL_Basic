using AutoMapper;
using LearningPlatform.Domain.SurveyDesign.LangageStrings;
using LearningPlatform.Domain.SurveyDesign.Resources;

namespace LearningPlatform.Domain.Mapping
{
    public class DomainAutoMapperProfile : Profile
    {
        public DomainAutoMapperProfile()
        {
            //TODO:...

            CreateMap<LanguageString, EvaluationString>().ConstructUsing(CreateEvaluationString);
            CreateMap<ResourceString, EvaluationString>().ConstructUsing(CreateEvaluationString);
        }

        //private void AfterLanguageSelectionQuestionMap(LanguageSelectionQuestionDefinition definition, LanguageSelectionQuestion question, ResolutionContext resolutionContext)
        //{
        //    Resolve<QuestionMapperService>(resolutionContext).MapLanguageSelectionQuestion(question, definition);
        //}

        private EvaluationString CreateEvaluationString(ILanguageString value, ResolutionContext resolutionContext)
        {
            return Resolve<LanguageService>(resolutionContext).CreateEvaluationString(value);
        }

        private static T Resolve<T>(ResolutionContext resolutionContext)
        {
            return (T)resolutionContext.Options.ServiceCtor(typeof(T));
        }

    }
}
