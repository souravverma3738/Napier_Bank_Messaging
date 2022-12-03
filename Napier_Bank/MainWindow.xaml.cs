using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
namespace Napier_Bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String header;
        public String body;
        public String msgsender;
        public string emailType;

        public static MsgList messagelist;
         static MsgList1 messagelist1;
        static MsgList_email messagelist_email;
        static IncidentReportList incidentList;


        public MainWindow()
        {
            InitializeComponent();
            cmbIncident.Items.Add("Theft");
            cmbIncident.Items.Add("Staff Attack");
            cmbIncident.Items.Add("ATM Theft");
            cmbIncident.Items.Add("Raid");
            cmbIncident.Items.Add("Customer Attack");
            cmbIncident.Items.Add("Staff Abuse");
            cmbIncident.Items.Add("Bomb Threat");
            cmbIncident.Items.Add("Terrorism");
            cmbIncident.Items.Add("Suspicious Incident");
            cmbIncident.Items.Add("Intelligence");
            cmbIncident.Items.Add("Cash Loss");
            cmbIncident.SelectedIndex = 0;
        }


        public void Button_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sortcodeRegex = @"^(\d){2}-(\d){2}-(\d){2}$";


                header = txtarea.Text;
                body = txtbody.Text;
                msgsender = txtsender.Text;
                String sort_code = txtsort.Text;
                string incident = cmbIncident.Text;

                String subject = txtsubject.Text;

                if (!string.IsNullOrEmpty(subject))
                {
                    emailType = subject.Substring(0, 3).ToUpper();
                }
                // check the lenght of id 
                if (header.Length == 10)
                {
                    if (header[0].Equals('S'))
                    {
                        sms sm = new sms();
                        sm.Sender = msgsender;
                        sm.Text = body;
                        Last1 ls = new Last1(header, msgsender, Last1.ConvertTextspeak(body));
                        Writetofile_sms(ls);
                        MessageBox.Show("success0");
                    }


                    else if (header[0].Equals('T'))
                    {

                        Twitter t = new Twitter();
                        t.Sender = msgsender;
                        t.Text = body;

                        // Create Tweet object
                        List tweet = new List(header, msgsender, List.ConvertTextspeak(body));
                        Writetofile(tweet);
                        tweet.WriteMentions(body);

                        //tweet.WriteHashtags(body);


                        // Find hashtags within body text and write to a file

                        MessageBox.Show("Success5");
                        // Find mentions within the body text and write to a file
                        //                            tweet.WriteMentions(body);

                        //                          clearFields();

                        // Convert textspeak, add to hashtag list, add to sender list

                    }

                    else if (header[0].Equals('E'))
                    {

                        email e1 = new email();
                        e1.Sender = msgsender;
                        e1.Subject = subject;
                        e1.Text = body;
                        Regex urlRegex = new Regex(@"\S+\.\S+");

                        if (subject[0].Equals('S'))
                        {
                            Match sortcodeValid = Regex.Match(sort_code, sortcodeRegex);

                            if (sortcodeValid.Success)
                            {
                                //QuarantineURL(body);
                                // Add to qList
                                // Return body text and use that in object creation


                                List_email.WriteUrlToFile(body);

                                // Replaces URLs with the text <URL Quarantined>
                                foreach (var foundURL in urlRegex.Matches(body))
                                {
                                    body = body.Replace(foundURL.ToString(), "<URL Quarantined>");
                                }

                                List_email email = new List_email(header, msgsender, subject, body);
                                Writetofile_email(email);

                                Sir sir = new Sir(header, subject, sort_code, incident);
                                WriteSIRToFile(sir);
                                MessageBox.Show("sir");

                            }
                            else
                            {
                                MessageBox.Show("Please ensure sort code is in the format XX-XX-XX.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Standard email");

                            List_email.WriteUrlToFile(body);

                            // Replaces URLs with the text <URL Quarantined>
                            foreach (var foundURL in urlRegex.Matches(body))
                            {
                                body = body.Replace(foundURL.ToString(), "<URL Quarantined>");
                            }

                            List_email email = new List_email(header, msgsender, subject, body);
                            Writetofile_email(email);
                        }
                    }
                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        private void WriteSIRToFile(Sir sirIn)
        {
            try
            {
                string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\jsons\sir.json";
                System.IO.FileInfo file = new System.IO.FileInfo(filepath);
                file.Directory.Create();

                if (File.Exists(filepath))
                {
                    incidentList = JsonConvert.DeserializeObject<IncidentReportList>(File.ReadAllText(filepath));
                    incidentList.Incidents.Add(sirIn);

                    File.WriteAllText(filepath, JsonConvert.SerializeObject(incidentList, Formatting.Indented) + Environment.NewLine);
                }
                // Else create a new file and write to it
                else
                {
                    File.WriteAllText(filepath, "{\"Incidents\": []}");

                    incidentList = JsonConvert.DeserializeObject<IncidentReportList>(File.ReadAllText(filepath));
                    incidentList.Incidents.Add(sirIn);

                    File.WriteAllText(filepath, JsonConvert.SerializeObject(incidentList, Formatting.Indented) + Environment.NewLine);
                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        private static void Writetofile_sms(Last1 filesms)
        {
            try
            {
                String filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\jsons\coursework_sms.json";
                System.IO.FileInfo file = new System.IO.FileInfo(filepath);

                file.Directory.Create();
                if (File.Exists(filepath))
                {
                    messagelist1 = JsonConvert.DeserializeObject<MsgList1>(File.ReadAllText(filepath));
                    messagelist1.list.Add(filesms);

                    File.WriteAllText(filepath, JsonConvert.SerializeObject(messagelist1, Formatting.Indented) + Environment.NewLine);
                }
                else
                {
                    File.WriteAllText(filepath, "{\"list\": []}");

                    messagelist1 = JsonConvert.DeserializeObject<MsgList1>(File.ReadAllText(filepath));
                    messagelist1.list.Add(filesms);

                    File.WriteAllText(filepath, JsonConvert.SerializeObject(messagelist1, Formatting.Indented) + Environment.NewLine);

                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private static void Writetofile(List twitter)
        {
            try
            {
                String filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\jsons\coursework_twitter.json";
                System.IO.FileInfo file = new System.IO.FileInfo(filepath);

                file.Directory.Create();
                if (File.Exists(filepath))
                {
                    messagelist = JsonConvert.DeserializeObject<MsgList>(File.ReadAllText(filepath));
                    messagelist.list.Add(twitter);

                    File.WriteAllText(filepath, JsonConvert.SerializeObject(messagelist, Formatting.Indented) + Environment.NewLine);
                }
                else
                {
                    File.WriteAllText(filepath, "{\"list\": []}");

                    messagelist = JsonConvert.DeserializeObject<MsgList>(File.ReadAllText(filepath));
                    messagelist.list.Add(twitter);

                    File.WriteAllText(filepath, JsonConvert.SerializeObject(messagelist, Formatting.Indented) + Environment.NewLine);

                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private static void Writetofile_email(List_email fileemail)
        {
            try
            {
                String filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\jsons\coursework_email.json";
                System.IO.FileInfo file = new System.IO.FileInfo(filepath);

                file.Directory.Create();
                if (File.Exists(filepath))
                {
                    messagelist_email = JsonConvert.DeserializeObject<MsgList_email>(File.ReadAllText(filepath));
                    messagelist_email.list.Add(fileemail);

                    File.WriteAllText(filepath, JsonConvert.SerializeObject(messagelist_email, Formatting.Indented) + Environment.NewLine);
                }
                else
                {
                    File.WriteAllText(filepath, "{\"list\": []}");

                    messagelist_email = JsonConvert.DeserializeObject<MsgList_email>(File.ReadAllText(filepath));
                    messagelist_email.list.Add(fileemail);

                    File.WriteAllText(filepath, JsonConvert.SerializeObject(messagelist_email, Formatting.Indented) + Environment.NewLine);

                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        

           
        }

        private void button_click1(object sender, RoutedEventArgs e)
        {
            HashtagList hashtag = new HashtagList();
            hashtag.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            URL le = new URL();
            le.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SIRList sl = new SIRList();
            sl.Show();
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MnetionList ml = new MnetionList();
            Close();
            ml.Show();
            
        }
    }

}
