using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static Napier_Bank.URL1;

namespace Napier_Bank
{ 
    class List_email
    {
        public string Header { get; set; }
        public string MsgSender { get; set; }
        public string Body { get; set; }
        public string subject { get; set; }

        public List_email(String headerIn, String senderIn, String bodyIn, String subjectIn)
        {
            Header = headerIn;
            MsgSender = senderIn;
            Body = bodyIn;
            subject = subjectIn;
        }

        public List<string> quarantineList = new List<string>();

        public static void WriteUrlToFile(string bodyIn)
        {
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\jsons\quarantine.json";
            System.IO.FileInfo file = new System.IO.FileInfo(filepath);
            file.Directory.Create();

            Regex urlRegex = new Regex(@"\S+\.\S+");

            if (File.Exists(filepath))
            {
                URLsList urlList = JsonConvert.DeserializeObject<URLsList>(File.ReadAllText(filepath));

                // For every URL found in the email body
                foreach (var foundURL in urlRegex.Matches(bodyIn))
                {
                    // Create a new entry in the JSON file for the URL
                    URL1 newQuarantine = new URL1(foundURL.ToString());
                    urlList.URLs.Add(newQuarantine);

                    // Write the URL list to the JSON file
                    File.WriteAllText(filepath, JsonConvert.SerializeObject(urlList, Formatting.Indented) + Environment.NewLine);
                }
            }
            // Else create a new file and write to it
            else
            {
                // Create new JSON file
                File.WriteAllText(filepath, "{\"URLs\": []}");
                URLsList urlList = JsonConvert.DeserializeObject<URLsList>(File.ReadAllText(filepath));

                // For every URL found in the email body
                foreach (var foundURL in urlRegex.Matches(bodyIn))
                {
                    // Create a new entry in the JSON file for the URL
                    URL1 newQuarantine = new URL1(foundURL.ToString());
                    urlList.URLs.Add(newQuarantine);
                }

                // Write new URLs to the JSON file
                File.WriteAllText(filepath, JsonConvert.SerializeObject(urlList, Formatting.Indented) + Environment.NewLine);
            }
        }
    }



    class MsgList_email
    {
        public List<List_email> list { get; set; }
    }
}
