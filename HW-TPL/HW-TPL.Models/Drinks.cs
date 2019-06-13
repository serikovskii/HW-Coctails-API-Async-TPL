using System;
using Newtonsoft.Json;

namespace HW_TPL.DTO
{
    public class Drinks
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonProperty("idDrink")]
        public string IdDrink { get; set; }
        [JsonProperty("strDrink")]
        public string NameDrink { get; set; }
        [JsonProperty("strDrinkThumb")]
        public string PhotoDrink { get; set; }
        
        [JsonProperty("strCategory")]
        public string CategoryDrink { get; set; }
        [JsonProperty("strAlcoholic")]
        public string AlcoholicFortresDrink { get; set; }
        [JsonProperty("strIngredient1")]
        public string IngredientDrink { get; set; }
        public int CountDrink { get; set; }
        public DateTime DateOrderDrink { get; set; } = DateTime.Now;
    }
}
