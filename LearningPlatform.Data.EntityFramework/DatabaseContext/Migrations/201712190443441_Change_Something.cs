namespace LearningPlatform.Data.EntityFramework.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Something : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.SurveyVersions", "SurveyId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SurveyVersions", new[] { "SurveyId" });
        }
    }
}
