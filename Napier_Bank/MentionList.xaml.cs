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
    /// Interaction logic for MnetionList.xaml
    /// </summary>
    public partial class MnetionList : Window
    {
        public MnetionList()
        {
            InitializeComponent();
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\jsons\mentions.json";
            System.IO.FileInfo file = new System.IO.FileInfo(filepath);
            file.Directory.Create();

            MentionsList mentList = JsonConvert.DeserializeObject<MentionsList>(File.ReadAllText(filepath));

            // Loop through Mention list
            foreach (Mention mention in mentList.Mentions)
            {
                lstMentions.Items.Add("" + mention.mention);
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
