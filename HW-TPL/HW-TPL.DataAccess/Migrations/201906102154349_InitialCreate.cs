namespace HW_TPL.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AllDrinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Drinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdDrink = c.String(),
                        NameDrink = c.String(),
                        PhotoDrink = c.String(),
                        CategoryDrink = c.String(),
                        AlcoholicFortresDrink = c.String(),
                        IngredientDrink = c.String(),
                        CountOrdering = c.Int(nullable: false),
                        AllDrinks_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AllDrinks", t => t.AllDrinks_Id)
                .Index(t => t.AllDrinks_Id);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IngredientName = c.String(),
                        IngredientCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Drinks", "AllDrinks_Id", "dbo.AllDrinks");
            DropIndex("dbo.Drinks", new[] { "AllDrinks_Id" });
            DropTable("dbo.Ingredients");
            DropTable("dbo.Drinks");
            DropTable("dbo.AllDrinks");
        }
    }
}
