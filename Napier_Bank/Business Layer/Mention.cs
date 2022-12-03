using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank
{
    class Mention
    {
        // Getters and setters
        public string mention { get; set; }


        public Mention(string mentionIn)
        {
            mention = mentionIn;
        }
    }

    class MentionsList
    {
        public List<Mention> Mentions { get; set; }
    }
}

