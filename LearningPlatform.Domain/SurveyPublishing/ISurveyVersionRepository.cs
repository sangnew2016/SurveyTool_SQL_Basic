using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Domain.SurveyPublishing
{
    public interface ISurveyVersionRepository
    {
        void Add(SurveyVersion surveyVersion);
        SurveyVersion GetLatest(long surveyId);
        List<SurveyVersion> GetAll(long surveyId);
    }
}
