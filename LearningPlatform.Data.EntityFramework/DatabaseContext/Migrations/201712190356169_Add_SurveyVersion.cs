namespace LearningPlatform.Data.EntityFramework.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SurveyVersion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurveyVersions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SurveyId = c.Long(nullable: false),
                        SerializedString = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SurveyVersions");
        }
    }
}
