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
    /// Interaction logic for HashtagList.xaml
    /// </summary>
    /// 
    public partial class HashtagList : Window
    {
        public HashtagList()
        {
            try
            {

                InitializeComponent();
                string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\jsons\hashtags.json";
                System.IO.FileInfo file = new System.IO.FileInfo(filepath);
                file.Directory.Create();

                HashList hashtagList = JsonConvert.DeserializeObject<HashList>(File.ReadAllText(filepath));

                List<Hashtag> hashlist = new List<Hashtag>();

                // Loop through Hashtag list
                foreach (Hashtag hashtag in hashtagList.hashtag1)
                {
                    lstHashtagList.Items.Add("Hashtag: " + hashtag.hashtag + "\nCount: " + hashtag.count);
                }
            }

            catch (Exception e)
            {
                MessageBox.Show("Message" + e.Message);
            }


        }
    
        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        }
        
}
