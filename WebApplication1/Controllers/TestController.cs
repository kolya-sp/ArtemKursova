﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

using Newtonsoft.Json;
using RestSharp.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TestController : ControllerBase
    {
        // GET: api/Test
        [HttpGet]
        public IEnumerable<string> Get()
        {
            try
            {
                Aforizm Res = new Aforizm();
                return new string[] { Res.Deserialize() };
            }
            catch
            {
                return new string[] { "" };
            }
        }

        // GET: api/Test/слово_пошуку
        [HttpGet("{id}", Name = "Get")]
        public async Task<string> Get(string id)
        {
            string rezult="";
            try
            {
                using var client = new HttpClient();
                var content = await client.GetStringAsync($"https://ru.wikipedia.org/w/api.php?action=query&format=json&list=search&srsearch={id}");
                wikisearch wiki = System.Text.Json.JsonSerializer.Deserialize<wikisearch>(content);
                for (int i = 0; i < wiki.query.search.Count; i++) rezult += $"{i + 1}) id= "+ wiki.query.search[i].pageid +" \n " + wiki.query.search[i].snippet.Replace("<span class=\"searchmatch\">", "").Replace("</span>", "").Replace("\u00A0", "") + "\n";                   
            }
            catch
            {
                rezult = "Не знайдено";
            }
            if (rezult=="") rezult = "Не знайдено";
            return rezult;
        }

        // GET: api/Test/nomer/id
        [HttpGet("nomer/" + "{id}", Name = "Get2")]
        public async Task<string> Get2(string id)
        {
            string rez = "";
            try
            {
                using var client = new HttpClient();
                var content = await client.GetStringAsync($"https://ru.wikipedia.org/w/api.php?action=query&prop=info&pageids={id}&inprop=url&format=json");
                idurl r = System.Text.Json.JsonSerializer.Deserialize<idurl>(content);
                if (r != null && r.query!=null) rez += r.query.pages[id].fullurl;
                if (rez == "") rez = "ссилку на статтю не знайдено";
            }
            catch
            {
                rez = "ссилку на статтю не знайдено";
            }
            return rez;
        }
       
        // GET: api/Test/hisory/id_чата
        [HttpGet("hisory/" + "{id}", Name = "Get5")]
        public async Task<string> Get5(string id)
        {
            string rezult = "";
            try
            {
                List<log> history = JsonConvert.DeserializeObject<List<log>>(System.IO.File.ReadAllText(@"user.json"));
                int k = 0;
                if (history == null) history = new List<log>();
                for (int i = 0; i < history.Count; i++)
                    if (history[i].id == int.Parse(id)) { rezult += "№" + (k++) + ") " + history[i].info + "\n"; }
            }
            catch
            {

            }
            return "Історія чату: \n" + rezult;
        }

        // POST: api/Test
        [HttpPost()]
        public async Task<string> Post([FromBody] log value)
        {
            try
            {
                List<log> history = JsonConvert.DeserializeObject<List<log>>(System.IO.File.ReadAllText(@"user.json"));
                if (history == null) history = new List<log>();
                history.Add(value);
                int n = 5000;
                if (history.Count > n)
                {
                    List<log> history2 = new List<log>();
                    for (int i = 0; i <= n / 2; i++)
                    {
                        history2.Add(history[history.Count - 1 - n / 2 + i]);
                    }
                    await System.IO.File.WriteAllTextAsync(@"user.json", JsonConvert.SerializeObject(history2, Formatting.Indented));
                }
                else
                    await System.IO.File.WriteAllTextAsync(@"user.json", JsonConvert.SerializeObject(history, Formatting.Indented));
            }
            catch
            {

            }
            return "add " + value.id + value.info;
        }

        // PUT: api/Test/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] log value)
        {
            try
            {
                List<log> history = JsonConvert.DeserializeObject<List<log>>(System.IO.File.ReadAllText(@"user.json"));
                int k = -1;
                int i;
                for (i = 0; i < history.Count; i++) if (history[i].id == value.id)
                    {
                        k++;
                        if (k == id) break;
                    }
                if (history[i].id == value.id)
                {
                    if (history == null) history = new List<log>();
                    else
                    {
                        history[i] = value;
                    }
                    await System.IO.File.WriteAllTextAsync(@"user.json", JsonConvert.SerializeObject(history, Formatting.Indented));
                }
            }
            catch
            {

            }
        }

        // DELETE: api/ApiWithActions/{id}/{id2}
        [HttpDelete("{id}/{id2}")]
        public async Task Delete(int id, int id2) // id - номер повідомлення, id2 - номер чату
        {
            try
            {
                List<log> history = JsonConvert.DeserializeObject<List<log>>(System.IO.File.ReadAllText(@"user.json"));
                
                int k = -1;
                int i;
                for (i = 0; i < history.Count; i++) if (history[i].id == id2)
                    {
                        k++;
                        if (k == id) break;
                    }

                if (history == null)
                {
                    history = new List<log>();
                }
                else
                {
                    if (id != -1)
                    {
                        if (history[i].id == id2) history.RemoveAt(i);
                    }
                    else
                    {
                        for (int j = history.Count - 1; j >= 0; j--) if (history[j].id == id2) history.RemoveAt(j);
                    }
                }
                await System.IO.File.WriteAllTextAsync(@"user.json", JsonConvert.SerializeObject(history, Formatting.Indented));
            }
            catch
            {

            }
        }
    }
}
