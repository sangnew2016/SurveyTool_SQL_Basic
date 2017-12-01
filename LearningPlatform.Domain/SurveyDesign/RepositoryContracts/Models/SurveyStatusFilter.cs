namespace LearningPlatform.Domain.SurveyDesign.RepositoryContracts.Models
{
    public class SurveyStatusFilter
    {
        public bool New { get; set; }
        public bool Open { get; set; }
        public bool Closed { get; set; }
        public bool TemprorarilyClosed { get; set; }
    }
}
