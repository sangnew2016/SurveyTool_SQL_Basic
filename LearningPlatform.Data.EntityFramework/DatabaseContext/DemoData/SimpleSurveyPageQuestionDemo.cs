using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.SurveyDesign;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts;

namespace LearningPlatform.Data.EntityFramework.DatabaseContext.DemoData
{
    public class SimpleSurveyPageQuestionDemo
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IRequestObjectProvider<SurveyContext> _surveyContextProvider;
        private readonly SurveyDesign.Factory _surveyDesignFactory;

        public SimpleSurveyPageQuestionDemo(ISurveyRepository surveyRepository, IRequestObjectProvider<SurveyContext> surveyContextProvider, SurveyDesign.Factory surveyDesignFactory)
        {
            _surveyRepository = surveyRepository;
            _surveyContextProvider = surveyContextProvider;
            _surveyDesignFactory = surveyDesignFactory;
        }

        public void InsertData()
        {
            const long surveyId = 1;
            if (_surveyRepository.Exists(surveyId)) return;

            //To Do
            var create = _surveyDesignFactory.Invoke(surveyId: surveyId, useDatabaseIds: true);

            var survey = create.Survey(
                create.Folder("Main Page",
                    create.Page(
                        callback: null,
                        questionDefinitions: create.ShortTextQuestion("FullName", "Full Name", "Please enter your full name")
                    ),
                    create.Page(
                        callback: null,
                        questionDefinitions: create.LongTextQuestion("TextQuestion", "Demo Text Question", "Please enter your answer")
                    ),
                    create.ThankYouPage()
                )
            );

            survey.Name = "Simple Survey";
            survey.UserId = "f6e021af-a6a0-4039-83f4-152595b4671a";

            _surveyRepository.Add(survey);
            _surveyContextProvider.Get().SaveChanges();
        }

        public void PublishSurvey()
        {
            //output to survey version
            //...
        }
    }
}
