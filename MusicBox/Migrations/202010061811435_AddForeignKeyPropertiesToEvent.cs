namespace MusicBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyPropertiesToEvent : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Events", name: "Genre_Id", newName: "GenreId");
            RenameColumn(table: "dbo.Events", name: "Performer_Id", newName: "PerformerId");
            RenameIndex(table: "dbo.Events", name: "IX_Performer_Id", newName: "IX_PerformerId");
            RenameIndex(table: "dbo.Events", name: "IX_Genre_Id", newName: "IX_GenreId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Events", name: "IX_GenreId", newName: "IX_Genre_Id");
            RenameIndex(table: "dbo.Events", name: "IX_PerformerId", newName: "IX_Performer_Id");
            RenameColumn(table: "dbo.Events", name: "PerformerId", newName: "Performer_Id");
            RenameColumn(table: "dbo.Events", name: "GenreId", newName: "Genre_Id");
        }
    }
}
