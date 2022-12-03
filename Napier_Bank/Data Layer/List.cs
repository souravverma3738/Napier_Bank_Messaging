using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Napier_Bank
{
    public class List
    {
        public string Header { get; set; }
        public string MsgSender { get; set; }
        public string Body { get; set; }
        public string hastag { get; set; }
        public int count { get; set; }

        public List(String headerIn, String senderIn, String bodyIn)
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

        public void WriteHashtags(string bodyIn)
        {
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\jsons\hashtags.json";
            System.IO.FileInfo file = new System.IO.FileInfo(filepath);
            file.Directory.Create();

            Regex hashRegex = new Regex(@"(?:(?<=\s)|^)#(\w*[A-Za-z_]+\w*)");

            if (File.Exists(filepath))
            {

                HashList hashtag = new HashList();
                 hashtag = JsonConvert.DeserializeObject<HashList>(File.ReadAllText(filepath));

                // For every hashtag found in the tweet body
                foreach (var foundHashtag in hashRegex.Matches(bodyIn))
                {
                    // Default state for this flag
                    bool hashtagInFile = true;
                    // Loop through every hashtag in the JSON file
                    foreach (Hashtag hash in hashtag.hashtag1.ToList())
                    {
                        // If the current hashtag in the JSON file matches the hashtag in the tweet body
                        if (hash.hashtag == foundHashtag.ToString())
                        {
                            // Add to the number of times this hashtag has been used
                            hash.count++;

                            // Flag that a hashtag has been found
                            hashtagInFile = true;

                            // Write the count update to the JSON file
                            File.WriteAllText(filepath, JsonConvert.SerializeObject(hashtag, Formatting.Indented) + Environment.NewLine);

                            //Exit loop
                            goto HashFound;
                        }
                        else
                        {
                            // If the current hashtag is not in the JSON file, set flag to false
                            hashtagInFile = false;
                        }
                    }
                    // If hashtag is not in the file
                    if (hashtagInFile == false)
                    {
                        // Create a new entry in the JSON file for the hashtag
                        Hashtag newHashtag = new Hashtag(foundHashtag.ToString(), 1);
                        hashtag.hashtag1.Add(newHashtag);

                        // Write the hashtag list to the JSON file
                        File.WriteAllText(filepath, JsonConvert.SerializeObject(hashtag, Formatting.Indented) + Environment.NewLine);
                    }

                HashFound:
                    continue;
                }
            }
            // Else create a new file and write to it
            else
            {
                // Create new JSON file
                File.WriteAllText(filepath, "{\"Hashtags\": []}");
                HashList hashtag = JsonConvert.DeserializeObject<HashList>(File.ReadAllText(filepath));

                // For every hashtag found in the tweet body
                foreach (var foundHashtag in hashRegex.Matches(bodyIn))
                {
                    // Create new hashtag object to be written to the JSON file
                    Hashtag newHashtag = new Hashtag(foundHashtag.ToString(), 1);
                    hashtag.hashtag1.Add(newHashtag);
                }

                // Write new hashtags to the JSON file
                File.WriteAllText(filepath, JsonConvert.SerializeObject(hashtag, Formatting.Indented) + Environment.NewLine);
            }
        }

        public void WriteMentions(string bodyIn)
        {
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\jsons\mentions.json";
            System.IO.FileInfo file = new System.IO.FileInfo(filepath);
            file.Directory.Create();

            Regex mentionRegex = new Regex(@"(?:(?<=\s)|^)@(\w*[A-Za-z_]+\w*)");

            if (File.Exists(filepath))
            {
                MentionsList mentList = JsonConvert.DeserializeObject<MentionsList>(File.ReadAllText(filepath));

                // For every mention found in the tweet body
                foreach (var foundMention in mentionRegex.Matches(bodyIn))
                {
                    // Create a new entry in the JSON file for the mention
                    Mention newMention = new Mention(foundMention.ToString());
                    mentList.Mentions.Add(newMention);

                    // Write the mention list to the JSON file
                    File.WriteAllText(filepath, JsonConvert.SerializeObject(mentList, Formatting.Indented) + Environment.NewLine);
                }
            }
            // Else create a new file and write to it
            else
            {
                // Create new JSON file
                File.WriteAllText(filepath, "{\"Mentions\": []}");
                MentionsList mentList = JsonConvert.DeserializeObject<MentionsList>(File.ReadAllText(filepath));

                // For every mention found in the tweet body
                foreach (var foundMention in mentionRegex.Matches(bodyIn))
                {
                    // Create new mention object to be written to the JSON file
                    Mention newMention = new Mention(foundMention.ToString());
                    mentList.Mentions.Add(newMention);
                }

                // Write new mentions to the JSON file
                File.WriteAllText(filepath, JsonConvert.SerializeObject(mentList, Formatting.Indented) + Environment.NewLine);
            }
        }


    }
    public class MsgList
        {
            public List<List> list { get; set; }
        }
    
}

