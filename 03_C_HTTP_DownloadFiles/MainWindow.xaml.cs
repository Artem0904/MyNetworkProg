using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
using static System.Net.WebRequestMethods;
using Path = System.IO.Path;

namespace _03_C_HTTP_DownloadFiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebClient client;
        string Address;
        string Category;
        string PhotoHeight;
        string PhotoWidth;
        string FilePath;

        bool pathIsOK = false;
        public MainWindow()
        {
            InitializeComponent();
           
            client = new WebClient();

            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            Category = CategoryTBox.Text;
            PhotoHeight = HeightTBox.Text;
            PhotoWidth = WidthTBox.Text;
            FilePath = PathTBox.Text;

            if (client.IsBusy)
            {
                MessageBox.Show("Web Client is busy! Try agin later...");
                return;
            }

            // TODO: перевіряти шлях на коректність перед завантаженням
            //if(!Path.Exists(FilePath))
            //{
            //    MessageBox.Show("FilePath not exists! Try agin...");
            //    return;
            //}
            Address = $"https://source.unsplash.com/random/{PhotoWidth}x{PhotoHeight}/?{Category}";
            DownloadFileAsync(Address);
            
        }

        private async void DownloadFileAsync(string address)
        {
            // вибір шляху збереження надати користувачу
            string destination = FilePath;

            // додати обробку помилок (try catch)
            try
            {
                await client.DownloadFileTaskAsync(address, destination); // своє ім'я ставити
                pathIsOK = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (pathIsOK)
            {
                MessageBox.Show("Download complete!");
                DownloadPBar.Value = 0;
            }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (pathIsOK)
                DownloadPBar.Value = e.ProgressPercentage;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
