namespace MusicBox.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Name) VALUES ('Pop')");
            Sql("INSERT INTO Genres (Name) VALUES ('Rock')");
            Sql("INSERT INTO Genres (Name) VALUES ('House')");
            Sql("INSERT INTO Genres (Name) VALUES ('R&B')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Id IN (1,2,3,4)");
        }
    }
}
