using System;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Constants;
using LearningPlatform.Domain.SurveyDesign;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts;
using LearningPlatform.Domain.SurveyPublishing;

namespace LearningPlatform.Data.EntityFramework.DatabaseContext.DemoData
{
    public class SimpleSurveyPageQuestionDemo
    {
        private readonly IRequestObjectProvider<SurveyContext> _surveyContextProvider;
        private readonly ISurveyRepository _surveyRepository;
        private readonly SurveyDefinitionService _surveyDefinitionService;
        private readonly SurveyDesign.Factory _surveyDesignFactory;
        private readonly SurveyPublishing.Factory _surveyPublishingFactory;

        public SimpleSurveyPageQuestionDemo(
            IRequestObjectProvider<SurveyContext> surveyContextProvider,
            ISurveyRepository surveyRepository,
            SurveyDefinitionService surveyDefinitionService,
            SurveyDesign.Factory surveyDesignFactory,
            SurveyPublishing.Factory surveyPublishingFactory)
        {
            _surveyContextProvider = surveyContextProvider;
            _surveyRepository = surveyRepository;
            _surveyDefinitionService = surveyDefinitionService;
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

        public void PublishSurvey(long surveyId)
        {
            //no need here (param maybe survey - for test)
            var survey = _surveyRepository.GetById(surveyId);
            if (survey == null)
            {
                throw new Exception("Survey does not exist");
            }

            //sorry, ambious to use delegate
            //insert surveyversion
            var publisher = _surveyPublishingFactory.Invoke();
            publisher.Publish(surveyId);

            //update status survey
            survey.LastPublished = DateTime.Now;
            survey.Status = SurveyStatus.Open;
            _surveyRepository.Update(survey);

            //save change (or API Action - save change)
            _surveyContextProvider.Get().SaveChanges();
        }
    }
}
