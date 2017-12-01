namespace LearningPlatform.Domain.SurveyDesign.RepositoryContracts.Models
{
    public class SurveySearchFilter
    {
        public string SearchString { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedDateTo { get; set; }
        public string CreatedDateOperator { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedDateTo { get; set; }
        public string ModifiedDateOperator { get; set; }
        public SurveyStatusFilter Status { get; set; }
        public bool ShowDeletedSurveys { get; set; }
    }
}
