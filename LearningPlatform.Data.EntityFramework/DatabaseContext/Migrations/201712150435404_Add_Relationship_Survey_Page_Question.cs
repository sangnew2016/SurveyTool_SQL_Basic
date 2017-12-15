namespace LearningPlatform.Data.EntityFramework.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Add_Relationship_Survey_Page_Question : DbMigration
    {
        public override void Up()
        {
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

            CreateTable(
                "dbo.Nodes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Alias = c.String(),
                        ParentId = c.Long(),
                        SurveyId = c.Long(nullable: false),
                        NodeType = c.String(),
                        RowVersion = c.Binary(),
                        Seed = c.Int(),
                        Seed1 = c.Int(),
                        Position = c.Int(),
                        ResponseStatus = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.ParentId)
                .Index(t => t.ParentId);

            CreateTable(
                "dbo.QuestionDefinitions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PageDefinitionId = c.Long(),
                        Alias = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        SurveyId = c.Long(nullable: false),
                        Position = c.Int(nullable: false),
                        RowVersion = c.Binary(),
                        Cols = c.Int(),
                        Rows = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.PageDefinitionId)
                .Index(t => t.PageDefinitionId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Surveys", "TopFolder_Id", "dbo.Nodes");
            DropForeignKey("dbo.QuestionDefinitions", "PageDefinitionId", "dbo.Nodes");
            DropForeignKey("dbo.Nodes", "ParentId", "dbo.Nodes");
            DropIndex("dbo.QuestionDefinitions", new[] { "PageDefinitionId" });
            DropIndex("dbo.Nodes", new[] { "ParentId" });
            DropIndex("dbo.Surveys", new[] { "TopFolder_Id" });
            DropIndex("dbo.Surveys", new[] { "UserId" });
            DropTable("dbo.QuestionDefinitions");
            DropTable("dbo.Nodes");
            DropTable("dbo.Surveys");
        }
    }
}
