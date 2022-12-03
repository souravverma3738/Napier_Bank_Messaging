using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Napier_Bank
{
    public class Last1
    {
        public string Header { get; set; }
        public string MsgSender { get; set; }
        public string Body { get; set; }

        public Last1(String headerIn, String senderIn, String bodyIn)
        {
            Header = headerIn;
            MsgSender = senderIn;
            Body = bodyIn;
        }



        public static string ConvertTextspeak(string bodyIn)
        {
            string bodyOut = bodyIn;

            Dictionary<string, string> textspeakList = File.ReadLines(@"C:\TestDirectory\textwords .csv").Select(x => x.Split(',')).ToDictionary(x => x[0], x => x[1]);

            // Loop through dictionary of textspeak abbreviations
            foreach (var currentAbbr in textspeakList)
            {
                string pattern = string.Format(@"\b{0}\b", Regex.Escape(currentAbbr.Key.ToLower()));
                string expanded = currentAbbr.Key + " <" + currentAbbr.Value + ">";

                bodyOut = Regex.Replace(bodyOut, pattern, expanded, RegexOptions.IgnoreCase);

            }
            return bodyOut;
        }


    }

     class MsgList1
    {
        public List<Last1> list { get; set; }
    }
}
