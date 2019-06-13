namespace HW_TPL.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitTwo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drinks", "CountDrink", c => c.Int(nullable: false));
            AddColumn("dbo.Drinks", "DateOrderDrink", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drinks", "DateOrderDrink");
            DropColumn("dbo.Drinks", "CountDrink");
        }
    }
}
