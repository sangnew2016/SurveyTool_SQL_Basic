namespace LearningPlatform.Data.EntityFramework.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSurveyModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Surveys", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Surveys", "IsDeleted");
        }
    }
}
