using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HW_TPL.DTO
{
    public class AllDrinks
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonProperty("drinks")]
        public List<Drinks> Drinks { get; set; }
    }
}
