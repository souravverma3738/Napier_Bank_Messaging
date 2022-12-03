        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank
{
    class URL1
    {
            // Getters and setters
            public string url { get; set; }


            public URL1(string urlIn)
            {
                url = urlIn;
            }
        
        public class URLsList
        {
            public List<URL1> URLs { get; set; }
        }
    }
}
