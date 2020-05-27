using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using System.Text;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;


namespace WebApplication1
{   
    public class Aforizm_predstavlenie
    {
        public string content { get; set; }
        public class org
        {
            public int id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
        }
        public org originator { get; set; }
    }
    public class Aforizm
    {
        public string Deserialize()
        {
            var client = new RestClient("https://quotes15.p.rapidapi.com/quotes/random/?language_code=ru");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "quotes15.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "cf31e88e60msh946062eb42bc84ap122dacjsn7762169b37e0");
            IRestResponse response = client.Execute(request);
            string content = response.Content;
            Aforizm_predstavlenie rezult = System.Text.Json.JsonSerializer.Deserialize<Aforizm_predstavlenie>(content);
            //return response.Content;
            return "" + Regex.Replace(rezult.content, @"\u00A0|\n", " ")+" Cказав "+ rezult.originator.name;
        }
    }
}

