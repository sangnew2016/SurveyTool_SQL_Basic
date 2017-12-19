namespace LearningPlatform.Data.EntityFramework.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Survey_Page_Question : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Nodes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Alias = c.String(nullable: false, maxLength: 256),
                        ParentId = c.Long(),
                        SurveyId = c.Long(nullable: false),
                        NodeType = c.String(maxLength: 30),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Seed = c.Int(),
                        Position = c.Int(),
                        ResponseStatus = c.String(maxLength: 30),
                        Title = c.String(),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.SurveyId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PageDefinitionId = c.Long(),
                        Alias = c.String(nullable: false, maxLength: 50),
                        Title = c.String(),
                        Description = c.String(),
                        SurveyId = c.Long(nullable: false),
                        Position = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Cols = c.Int(),
                        Rows = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.PageDefinitionId)
                .Index(t => t.PageDefinitionId)
                .Index(t => t.SurveyId);
            
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        LastPublished = c.DateTime(),
                        UserId = c.String(maxLength: 36, unicode: false),
                        TopFolder_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.TopFolder_Id)
                .Index(t => t.UserId)
                .Index(t => t.TopFolder_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Surveys", "TopFolder_Id", "dbo.Nodes");
            DropForeignKey("dbo.Questions", "PageDefinitionId", "dbo.Nodes");
            DropForeignKey("dbo.Nodes", "ParentId", "dbo.Nodes");
            DropIndex("dbo.Surveys", new[] { "TopFolder_Id" });
            DropIndex("dbo.Surveys", new[] { "UserId" });
            DropIndex("dbo.Questions", new[] { "SurveyId" });
            DropIndex("dbo.Questions", new[] { "PageDefinitionId" });
            DropIndex("dbo.Nodes", new[] { "SurveyId" });
            DropIndex("dbo.Nodes", new[] { "ParentId" });
            DropTable("dbo.Surveys");
            DropTable("dbo.Questions");
            DropTable("dbo.Nodes");
        }
    }
}
