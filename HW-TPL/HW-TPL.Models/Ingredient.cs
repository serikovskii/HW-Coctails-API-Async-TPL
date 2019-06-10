using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_TPL.Models
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
