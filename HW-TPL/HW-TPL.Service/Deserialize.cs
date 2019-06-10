using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HW_TPL.Models;
using Newtonsoft.Json;

namespace HW_TPL.Service
{
    public class Deserialize
    {
        public string Download(string path)
        {
            WebClient client = new WebClient();
            client.Headers.Set("X-RapidAPI-Host", "the-cocktail-db.p.rapidapi.com");
            client.Headers.Set("X-RapidAPI-Key", "74e940a6ebmshba7e1925271aec1p1e184djsn4c67d43481a2");
            string json = client.DownloadString(path);
            return json;
        }

        public AllDrinks ExecuteAllDrinks(int? filter, string type, string id)
        {
            AllDrinks result;
            if (filter == 1)
                result = JsonConvert.DeserializeObject<AllDrinks>(Download($"https://the-cocktail-db.p.rapidapi.com/filter.php?a={type}"));
            else if (filter == 0)
                result = JsonConvert.DeserializeObject<AllDrinks>(Download($"https://the-cocktail-db.p.rapidapi.com/filter.php?c={type}"));
            else
                result = JsonConvert.DeserializeObject<AllDrinks>(Download($"https://the-cocktail-db.p.rapidapi.com/lookup.php?i={id}"));
            return result;
        }

        public Filter ExecuteFilterName(int filter)
        {
            Filter result;
            if (filter == 1)
                result = JsonConvert.DeserializeObject<Filter>(Download("https://the-cocktail-db.p.rapidapi.com/list.php?a=list"));
            else
                result = JsonConvert.DeserializeObject<Filter>(Download("https://the-cocktail-db.p.rapidapi.com/list.php?c=list"));
            return result;
        }
    }
}
