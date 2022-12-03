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
using static Napier_Bank.URL1;

namespace Napier_Bank
{
    /// <summary>
    /// Interaction logic for URL.xaml
    /// </summary>
    public partial class URL : Window
    {
        public URL()
        {
            InitializeComponent();
            string sirFilepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\jsons\quarantine.json";

            System.IO.FileInfo sirFile = new System.IO.FileInfo(sirFilepath);
            sirFile.Directory.Create();

            URLsList incidentList = JsonConvert.DeserializeObject<URLsList>(File.ReadAllText(sirFilepath));



            // Loop through SIR emails
            foreach (URL1 ur in incidentList.URLs)
            {
                // Loop through Messages
                // If there is a Message with an associated SIR email, display in the listbox
                lst_url.Items.Add("" + ur.url);

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