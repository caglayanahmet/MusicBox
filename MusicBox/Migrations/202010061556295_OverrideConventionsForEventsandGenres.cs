namespace MusicBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverrideConventionsForEventsandGenres : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.Events", "Performer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "Genre_Id" });
            DropIndex("dbo.Events", new[] { "Performer_Id" });
            AlterColumn("dbo.Events", "Address", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Events", "Genre_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "Performer_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Genres", "Name", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Events", "Genre_Id");
            CreateIndex("dbo.Events", "Performer_Id");
            AddForeignKey("dbo.Events", "Genre_Id", "dbo.Genres", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Events", "Performer_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Performer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Events", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Events", new[] { "Performer_Id" });
            DropIndex("dbo.Events", new[] { "Genre_Id" });
            AlterColumn("dbo.Genres", "Name", c => c.String());
            AlterColumn("dbo.Events", "Performer_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Events", "Genre_Id", c => c.Int());
            AlterColumn("dbo.Events", "Address", c => c.String());
            CreateIndex("dbo.Events", "Performer_Id");
            CreateIndex("dbo.Events", "Genre_Id");
            AddForeignKey("dbo.Events", "Performer_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Events", "Genre_Id", "dbo.Genres", "Id");
        }
    }
}
