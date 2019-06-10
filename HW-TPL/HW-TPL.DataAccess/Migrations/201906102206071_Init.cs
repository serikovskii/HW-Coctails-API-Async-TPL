namespace HW_TPL.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Drinks", "CountOrdering");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Drinks", "CountOrdering", c => c.Int(nullable: false));
        }
    }
}
