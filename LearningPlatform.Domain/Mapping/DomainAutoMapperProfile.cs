using AutoMapper;

namespace LearningPlatform.Domain.Mapping
{
    public class DomainAutoMapperProfile : Profile
    {
        public DomainAutoMapperProfile()
        {
            //TODO:...

        }

        //private void AfterLanguageSelectionQuestionMap(LanguageSelectionQuestionDefinition definition, LanguageSelectionQuestion question, ResolutionContext resolutionContext)
        //{
        //    Resolve<QuestionMapperService>(resolutionContext).MapLanguageSelectionQuestion(question, definition);
        //}

        //private EvaluationString CreateEvaluationString(ILanguageString value, ResolutionContext resolutionContext)
        //{
        //    return Resolve<LanguageService>(resolutionContext).CreateEvaluationString(value);
        //}

        private static T Resolve<T>(ResolutionContext resolutionContext)
        {
            return (T)resolutionContext.Options.ServiceCtor(typeof(T));
        }

    }
}
