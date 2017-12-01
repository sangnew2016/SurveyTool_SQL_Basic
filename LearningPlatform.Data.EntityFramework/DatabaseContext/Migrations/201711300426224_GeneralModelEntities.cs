namespace LearningPlatform.Data.EntityFramework.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class GeneralModelEntities : DbMigration
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
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        UserId = c.String(maxLength: 36, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserId);

        }

        public override void Down()
        {
            DropIndex("dbo.Surveys", new[] { "UserId" });
            DropTable("dbo.Surveys");
        }
    }
}
