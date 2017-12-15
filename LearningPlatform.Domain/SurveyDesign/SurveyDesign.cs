using LearningPlatform.Domain.Constants;
using LearningPlatform.Domain.SurveyDesign.Pages;
using LearningPlatform.Domain.SurveyDesign.Questions;
using System;
using System.Collections.Generic;

namespace LearningPlatform.Domain.SurveyDesign
{
    public class SurveyDesign
    {

        //Mot Delegate de ben file hkac xai ma khong
        public delegate SurveyDesign Factory(long? surveyId = null, bool useDatabaseIds = false);

        private readonly Dictionary<string, Folder> _folders = new Dictionary<string, Folder>();
        private bool _useDatabaseIds;
        private Survey _survey;
        private long _nodeId;
        private int _questionId;

        public SurveyDesign(long? surveyId, bool useDatabaseIds)
        {
            _survey = new Survey()
            {
                Status = SurveyStatus.New,
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            if (surveyId.HasValue) _survey.Id = surveyId.Value;

            _useDatabaseIds = useDatabaseIds;
        }

        public long SurveyId => _survey.Id;

        public Survey Survey(string surveyModelName, string userId)
        {
            _survey.Name = surveyModelName;
            _survey.UserId = userId;
            return _survey;
        }

        public Survey Survey(Folder topFolder = null, Action<Survey> callback = null)
        {
            callback?.Invoke(_survey);

            if (topFolder != null)
            {
                _survey.TopFolder = topFolder;
            }
            return _survey;
        }

        public Folder Folder(string name, params Node[] nodes)
        {
            Folder folder = GetFolder(name);
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    folder.ChildNodes.Add(node);
                }
            }
            return folder;
        }

        public PageDefinition Page(Action<PageDefinition> callback, params QuestionDefinition[] questionDefinitions)
        {
            ++_nodeId;
            var pageDefinition = new PageDefinition(questionDefinitions)
            {
                Id = _useDatabaseIds ? 0 : _nodeId,
                Alias = "Page" + _nodeId,
                Title = "Page " + _nodeId,
                Description = "",
                SurveyId = SurveyId,
            };
            if (!_useDatabaseIds)
            {
                foreach (var questionDefinition in questionDefinitions)
                {
                    questionDefinition.PageDefinition = pageDefinition;
                    questionDefinition.PageDefinitionId = pageDefinition.Id;
                }
            }
            callback?.Invoke(pageDefinition);

            return pageDefinition;
        }

        public Node ThankYouPage()
        {
            return Page(p =>
            {
                p.Title = "Thank you page";
                p.NodeType = PageType.ThankYouPage.ToString();
                p.Alias = SurveyConstants.THANK_YOU_PAGE_NAME;
            }, Information("thankyou", "Thank you!", "You have completed the survey."));
        }

        public ShortTextQuestionDefinition ShortTextQuestion(string name, string title = "", string description = "")
        {
            return CreateOpenEndedTextQuestionDefinition<ShortTextQuestionDefinition>(name, title, description);
        }

        public LongTextQuestionDefinition LongTextQuestion(string name, string title = "", string description = "")
        {
            return CreateOpenEndedTextQuestionDefinition<LongTextQuestionDefinition>(name, title, description);
        }

        private T CreateOpenEndedTextQuestionDefinition<T>(string name, string title, string description) where T : OpenEndedTextQuestionDefinition, new()
        {
            var openEndedTextQuestionDefinition = new T
            {
                Id = _useDatabaseIds ? 0 : ++_questionId,
                Alias = name,
                Title = title,
                Description = description,
                SurveyId = SurveyId
            };

            return openEndedTextQuestionDefinition;
        }

        private Folder GetFolder(string name)
        {
            Folder folder;
            if (!_folders.TryGetValue(name, out folder))
            {
                folder = new Folder(name)
                {
                    Id = _useDatabaseIds ? 0 : ++_nodeId,
                    SurveyId = SurveyId
                };
                _folders[name] = folder;
            }
            return folder;
        }

        public InformationDefinition Information(string name, string title, string description)
        {
            return new InformationDefinition
            {
                Id = _useDatabaseIds ? 0 : ++_questionId,
                Alias = name,
                Title = title,
                Description = description,
                SurveyId = SurveyId
            };
        }
    }
}
