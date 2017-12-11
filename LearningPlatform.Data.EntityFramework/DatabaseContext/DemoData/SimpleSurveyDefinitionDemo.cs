using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.SurveyDesign;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts;

namespace LearningPlatform.Data.EntityFramework.DatabaseContext.DemoData
{
    public class SimpleSurveyDefinitionDemo
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IRequestObjectProvider<SurveyContext> _surveyContextProvider;
        private readonly SurveyDesign.Factory _surveyDesignFactory;

        public SimpleSurveyDefinitionDemo(ISurveyRepository surveyRepository, IRequestObjectProvider<SurveyContext> surveyContextProvider, SurveyDesign.Factory surveyDesignFactory)
        {
            _surveyRepository = surveyRepository;
            _surveyContextProvider = surveyContextProvider;
            _surveyDesignFactory = surveyDesignFactory;
        }

        public void InsertData()
        {
            const long surveyId = 1;
            if (_surveyRepository.Exists(surveyId)) return;

            //============================
            // EX: use multi language
            //============================
            var create = _surveyDesignFactory.Invoke(surveyId: surveyId, useDatabaseIds: true, language: "en");
            var survey = create.Survey(
                surveyModelName: "Simple Survey",
                userId: "f6e021af-a6a0-4039-83f4-152595b4671a",
                title: new [] {"Simple survey title", "vn::Tieu de khao sat don gian"},
                description: new [] {"Simple survey description", "vn::Mo ta khao sat don gian"});

            _surveyRepository.Add(survey);
            _surveyContextProvider.Get().SaveChanges();

            //============================
            // EX: javascript inside C#
            //============================

        }
    }
}
