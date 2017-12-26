using Autofac;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Exceptions;
using LearningPlatform.Domain.SurveyDesign;
using LearningPlatform.Domain.SurveyDesign.Pages;
using LearningPlatform.Domain.SurveyDesign.RepositoryContracts;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LearningPlatform.Domain.SurveyPublishing
{
    public class PublishingService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyVersionRepository _surveyVersionRepository;
        private readonly JsonSerializerSettings _serializerSettings;

        public PublishingService(ISurveyRepository surveyRepository, ISurveyVersionRepository surveyVersionRepository, IComponentContext componentContext)
        {
            _surveyRepository = surveyRepository;
            _surveyVersionRepository = surveyVersionRepository;
            _serializerSettings = JsonSerializerSettingsFactory.Create(componentContext);
        }


        public void Publish(long surveyId)
        {
            var surveyAndLayout = GetUnpublishedVersion(surveyId);
            var surveyVersion = new SurveyVersion
            {
                SurveyId = surveyId,
                SerializedString = JsonConvert.SerializeObject(surveyAndLayout, Formatting.Indented, _serializerSettings)
            };
            _surveyVersionRepository.Add(surveyVersion);            
        }

        public Survey GetLatestVersion(long surveyId)
        {
            SurveyVersion surveyVersion = GetSurveyVersion(surveyId);
            return JsonConvert.DeserializeObject<Survey>(surveyVersion.SerializedString, _serializerSettings);
        }

        public Survey GetUnpublishedVersion(long surveyId)
        {
            var survey = _surveyRepository.GetById(surveyId);
            if (survey == null) throw new EntityNotFoundException($"Survey {surveyId} not found"); //TODO: create own exception

            var pageDefinitions = new List<PageDefinition>();
            BuildPages(survey.TopFolder, pageDefinitions);

            return survey;
        }

        private static void BuildPages(Folder folder, List<PageDefinition> pages)
        {
            foreach (var node in folder.ChildNodes)
            {
                var childFolder = node as Folder;
                if (childFolder != null)
                {
                    BuildPages(childFolder, pages);
                }
                var page = node as PageDefinition;
                if (page != null) pages.Add(page);
            }
        }

        private SurveyVersion GetSurveyVersion(long surveyId)
        {
            var surveyVersion = _surveyVersionRepository.GetLatest(surveyId);
            if (surveyVersion == null) throw new EntityNotFoundException($"Survey version {surveyId} not found."); //TODO: create own exception
            return surveyVersion;
        }

        public bool IsPublished(long surveyId)
        {
            return _surveyVersionRepository.GetLatest(surveyId) != null;
        }
    }
}
