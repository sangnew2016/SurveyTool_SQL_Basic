namespace LearningPlatform.Domain.SurveyDesign.Resources
{
    public interface IResourceManager
    {
        string GetString(string name, params object[] args);
        EvaluationString GetEvalutationString(string name);
    }
}
