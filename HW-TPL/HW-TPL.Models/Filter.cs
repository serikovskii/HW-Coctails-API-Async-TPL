using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_TPL.Models
{
    public class Filter
    {
        [JsonProperty("drinks")]
        public List<FilterName> FilterNames { get; set; }
    }
}
