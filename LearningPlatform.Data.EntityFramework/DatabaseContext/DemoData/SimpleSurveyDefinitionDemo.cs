using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.SurveyDesign;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //To Do
            var create = _surveyDesignFactory.Invoke(surveyId: surveyId, useDatabaseIds: true);
            var survey = create.Survey("Simple Survey", "f6e021af-a6a0-4039-83f4-152595b4671a");

            _surveyRepository.Add(survey);
            _surveyContextProvider.Get().SaveChanges();
        }
    }
}
