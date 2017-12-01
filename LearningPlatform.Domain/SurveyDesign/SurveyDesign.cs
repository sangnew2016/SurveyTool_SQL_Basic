using LearningPlatform.Domain.Constants;
using System;

namespace LearningPlatform.Domain.SurveyDesign
{
    public class SurveyDesign
    {

        //Mot Delegate de ben file hkac xai ma khong
        public delegate SurveyDesign Factory(long? surveyId = null, bool useDatabaseIds = false);

        private bool _useDatabaseIds;
        private Survey _survey;

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
    }
}
