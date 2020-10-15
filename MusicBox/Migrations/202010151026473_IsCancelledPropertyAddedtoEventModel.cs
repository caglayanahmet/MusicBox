namespace MusicBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsCancelledPropertyAddedtoEventModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "IsCancelled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "IsCancelled");
        }
    }
}
