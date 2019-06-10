using Newtonsoft.Json;

namespace HW_TPL.Models
{
    public class FilterName
    {
        [JsonProperty("strCategory")]
        public string NameCategory { get; set; }
        [JsonProperty("strAlcoholic")]
        public string NameAlcohol { get; set; }
    }
}