namespace MusicBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEventTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Address = c.String(),
                        Genre_Id = c.Int(),
                        Performer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.Genre_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Performer_Id)
                .Index(t => t.Genre_Id)
                .Index(t => t.Performer_Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Performer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Events", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Events", new[] { "Performer_Id" });
            DropIndex("dbo.Events", new[] { "Genre_Id" });
            DropTable("dbo.Genres");
            DropTable("dbo.Events");
        }
    }
}
