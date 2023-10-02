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
        string DirecoryPath;

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
            DirecoryPath = PathTBox.Text;

            if (client.IsBusy)
            {
                MessageBox.Show("Web Client is busy! Try agin later...");
                return;
            }

            // TODO: перевіряти шлях на коректність перед завантаженням
            if(!Directory.Exists(DirecoryPath))
            {
                MessageBox.Show("Directory not exists! Try agin...");
                return;
            }
            Address = $"https://source.unsplash.com/random/{PhotoWidth}x{PhotoHeight}/?{Category}";
            DownloadFileAsync(Address);
        }

        private async void DownloadFileAsync(string address)
        {
            // вибір шляху збереження надати користувачу
            string fileName = Path.GetFileName(address);
            string destination = Path.Combine(DirecoryPath, fileName);

            // додати обробку помилок (try catch)
            try
            {
                await client.DownloadFileTaskAsync(address, destination); // своє ім'я ставити
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download complete!");
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadPBar.Value = e.ProgressPercentage;
        }
    }
}
