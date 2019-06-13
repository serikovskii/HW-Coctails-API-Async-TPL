namespace HW_TPL.DataAccess
{
    using HW_TPL.DTO;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DrinkContext : DbContext
    {
        public DrinkContext()
            : base("name=DrinkContext")
        {
        }
        public DbSet<AllDrinks> AllDrinksess { get; set; }
        public DbSet<Drinks> Drinksess { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }

}