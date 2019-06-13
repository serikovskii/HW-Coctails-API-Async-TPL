using System;

namespace HW_TPL.DTO
{
    public class Ingredient
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string IngredientName { get; set; }
        public int IngredientCount { get; set; }
        public Ingredient()
        {
            IngredientCount = 10;
        }
    }
}
