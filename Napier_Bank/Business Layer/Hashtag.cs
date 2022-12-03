    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank
{
    class Hashtag
    {
        // Getters and setters
        public string hashtag { get; set; }
        public int count { get; set; }


        public Hashtag(string hashtagIn, int countIn)
        {
            hashtag = hashtagIn;
            count = countIn;
        }
    }

   class HashList
    {
        public List<Hashtag> hashtag1 { get; set; }
    }
}

