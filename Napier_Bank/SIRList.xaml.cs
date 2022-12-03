using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Napier_Bank
{
    /// <summary>
    /// Interaction logic for SIR.xaml
    /// </summary>
    public partial class SIRList : Window
    {
        static IncidentReportList incidentList;

        public SIRList()
        {
            InitializeComponent();
            string sirFilepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\jsons\sir.json";

            System.IO.FileInfo sirFile = new System.IO.FileInfo(sirFilepath);
            sirFile.Directory.Create();



            IncidentReportList incidentList = JsonConvert.DeserializeObject<IncidentReportList>(File.ReadAllText(sirFilepath));

            // Loop through SIR emails
            foreach (Sir sir in incidentList.Incidents)
            {
                // Loop through Messages
                // If there is a Message with an associated SIR email, display in the listbox
                lstSIR.Items.Add("" + "Incident: " + sir.incident + "\n" + "Sort Code: " + sir.sortcode);
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow we = new MainWindow();
            Close();
            we.Show();
        }
    } 

    }