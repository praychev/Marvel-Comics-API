namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Age = c.Int(nullable: false),
                        Gender = c.String(maxLength: 10),
                        Description = c.String(maxLength: 250),
                        ComicsId = c.Int(nullable: false),
                        SeriesId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.Int(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comics", t => t.ComicsId, cascadeDelete: true)
                .ForeignKey("dbo.Series", t => t.SeriesId, cascadeDelete: true)
                .Index(t => t.ComicsId)
                .Index(t => t.SeriesId);
            
            CreateTable(
                "dbo.Comics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        IssueNumber = c.Int(nullable: false),
                        Description = c.String(maxLength: 50),
                        ISBN = c.String(nullable: false, maxLength: 10),
                        PageCount = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.Int(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 50),
                        StartYear = c.Int(nullable: false),
                        EndYear = c.Int(nullable: false),
                        Rating = c.Double(nullable: false),
                        ComicsId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.Int(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comics", t => t.ComicsId, cascadeDelete: false)
                .Index(t => t.ComicsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Series", "ComicsId", "dbo.Comics");
            DropForeignKey("dbo.Characters", "SeriesId", "dbo.Series");
            DropForeignKey("dbo.Characters", "ComicsId", "dbo.Comics");
            DropIndex("dbo.Series", new[] { "ComicsId" });
            DropIndex("dbo.Characters", new[] { "SeriesId" });
            DropIndex("dbo.Characters", new[] { "ComicsId" });
            DropTable("dbo.Series");
            DropTable("dbo.Comics");
            DropTable("dbo.Characters");
        }
    }
}