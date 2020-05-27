using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class wikisearch
    {
        public class k1
        {
            public class rez
            {
                public string title { get; set; }
                public int pageid { get; set; }
                public string snippet { get; set; }
                // "An <span class=\"searchmatch\">apple</span> is an edible fruit produced by an <span class=\"searchmatch\">apple</span> tree (Malus domestica). <span class=\"searchmatch\">Apple</span> trees are cultivated worldwide and are the most widely grown species in",
            }
            public List<rez> search { get; set; }
        }
        public k1 query { get; set; }
    }
}
