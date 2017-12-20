using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Constants;
using LearningPlatform.Domain.SurveyDesign;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts;
using LearningPlatform.Domain.SurveyPublishing;

namespace LearningPlatform.Data.EntityFramework.DatabaseContext.DemoData
{
    public class SimpleSurveyPageQuestionDemo
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IRequestObjectProvider<SurveyContext> _surveyContextProvider;
        private readonly SurveyDesign.Factory _surveyDesignFactory;
        private readonly SurveyPublishing.Factory _surveyPublishingFactory;

        public SimpleSurveyPageQuestionDemo(
            ISurveyRepository surveyRepository,
            IRequestObjectProvider<SurveyContext> surveyContextProvider,
            SurveyDesign.Factory surveyDesignFactory,
            SurveyPublishing.Factory surveyPublishingFactory)
        {
            _surveyRepository = surveyRepository;
            _surveyContextProvider = surveyContextProvider;
            _surveyDesignFactory = surveyDesignFactory;
            _surveyPublishingFactory = surveyPublishingFactory;
        }

        public Survey InsertData()
        {
            var surveyId = SurveyConstants.SimpleSurveyPageQuestionSurveyId;
            if (_surveyRepository.Exists(surveyId)) return null;

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

            return survey;
        }

        public Survey PublishSurvey(long surveyId)
        {
            //output to survey version
            var publisher = _surveyPublishingFactory.Invoke();
            var survey = publisher.Publish(surveyId);
            _surveyContextProvider.Get().SaveChanges();

            return survey;
        }
    }
}
