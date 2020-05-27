using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Resept
    {
        public class resept1
        {
            public string title { get; set; }
            public string sourceUrl { get; set; }       
        }
        public List<resept1> results { get; set; }
    }
}
