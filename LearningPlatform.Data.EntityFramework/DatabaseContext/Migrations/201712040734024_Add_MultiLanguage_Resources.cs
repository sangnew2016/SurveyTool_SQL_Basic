namespace LearningPlatform.Data.EntityFramework.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_MultiLanguage_Resources : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LanguageStrings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SurveyId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SurveyId);
            
            CreateTable(
                "dbo.LanguageStringItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Language = c.String(),
                        Text = c.String(nullable: false),
                        LanguageStringId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LanguageStrings", t => t.LanguageStringId, cascadeDelete: true)
                .Index(t => t.LanguageStringId);
            
            CreateTable(
                "dbo.ResourceItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Language = c.String(),
                        Text = c.String(nullable: false),
                        ResourceStringId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resources", t => t.ResourceStringId, cascadeDelete: true)
                .Index(t => t.ResourceStringId);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SurveyId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, name: "ix_ResourceString_Name");
            
            AddColumn("dbo.Surveys", "Description_Id", c => c.Long());
            AddColumn("dbo.Surveys", "Title_Id", c => c.Long());
            CreateIndex("dbo.Surveys", "Description_Id");
            CreateIndex("dbo.Surveys", "Title_Id");
            AddForeignKey("dbo.Surveys", "Description_Id", "dbo.LanguageStrings", "Id");
            AddForeignKey("dbo.Surveys", "Title_Id", "dbo.LanguageStrings", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Surveys", "Title_Id", "dbo.LanguageStrings");
            DropForeignKey("dbo.Surveys", "Description_Id", "dbo.LanguageStrings");
            DropForeignKey("dbo.ResourceItems", "ResourceStringId", "dbo.Resources");
            DropForeignKey("dbo.LanguageStringItems", "LanguageStringId", "dbo.LanguageStrings");
            DropIndex("dbo.Surveys", new[] { "Title_Id" });
            DropIndex("dbo.Surveys", new[] { "Description_Id" });
            DropIndex("dbo.Resources", "ix_ResourceString_Name");
            DropIndex("dbo.ResourceItems", new[] { "ResourceStringId" });
            DropIndex("dbo.LanguageStringItems", new[] { "LanguageStringId" });
            DropIndex("dbo.LanguageStrings", new[] { "SurveyId" });
            DropColumn("dbo.Surveys", "Title_Id");
            DropColumn("dbo.Surveys", "Description_Id");
            DropTable("dbo.Resources");
            DropTable("dbo.ResourceItems");
            DropTable("dbo.LanguageStringItems");
            DropTable("dbo.LanguageStrings");
        }
    }
}
