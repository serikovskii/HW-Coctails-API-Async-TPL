using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_TPL.Models
{
    public class AllDrinks
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonProperty("drinks")]
        public List<Drinks> Drinks { get; set; }
    }
}
