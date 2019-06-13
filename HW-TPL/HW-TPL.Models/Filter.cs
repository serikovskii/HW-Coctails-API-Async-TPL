using Newtonsoft.Json;
using System.Collections.Generic;

namespace HW_TPL.DTO
{
    public class Filter
    {
        [JsonProperty("drinks")]
        public List<FilterName> FilterNames { get; set; }
    }
}
