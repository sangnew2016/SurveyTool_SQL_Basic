using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Constants;
using Newtonsoft.Json;
using System;
using LearningPlatform.Domain.SurveyDesign.LangageStrings;

namespace LearningPlatform.Domain.SurveyDesign
{
    public class Survey : IVersionable
    {
        public long Id { get; set; }

        public string Name { get; set; }

        //=============================================
        //set language
        //=============================================
        public LanguageString Title { get; set; }
        public LanguageString Description { get; set; }

        public SurveyStatus Status { get; set; }

        [JsonIgnore]
        public bool IsSurveyClosed
        {
            get
            {
                return Status == SurveyStatus.Closed || Status == SurveyStatus.TemprorarilyClosed;
            }
        }

        public bool IsDeleted { get; set; }

        public byte[] RowVersion { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public string UserId { get; set; }
    }
}