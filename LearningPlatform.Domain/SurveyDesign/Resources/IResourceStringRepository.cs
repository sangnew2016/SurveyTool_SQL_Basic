using System.Collections.Generic;

namespace LearningPlatform.Domain.SurveyDesign.Resources
{
    public interface IResourceStringRepository
    {
        void AddOrUpdate(ResourceString resourceString);
        IList<ResourceString> GetByNameForSurvey(string name, long surveyId);
    }
}
