using Newtonsoft.Json;

namespace HW_TPL.DTO
{
    public class FilterName
    {
        [JsonProperty("strCategory")]
        public string NameCategory { get; set; }
        [JsonProperty("strAlcoholic")]
        public string NameAlcohol { get; set; }
    }
}