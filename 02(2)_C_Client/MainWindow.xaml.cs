using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
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
using _02_1__SharedData;
using System.Text.Json;

namespace _02_2__C_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int port = 8080;              // порт сервера
        static string address = "127.0.0.1"; // адреса сервера
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            TcpClient client = new TcpClient();

            // підключення до віддаленого хоста
            client.Connect(ipPoint);

            try
            {
                string VinNumber = string.Empty;



                // введення даних для відправки
                VinNumber = VINTBox.Text;
                VINTBox.Text = string.Empty;

                // отримуємо потік для обміну повідомленнями
                NetworkStream ns = client.GetStream();

                // серіалізація об'єкта та відправка його
                //JsonSerializer.Serialize(ns, VinNumber);

                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(VinNumber);

                sw.Flush();

                // отримуємо відповідь

                StreamReader sr = new StreamReader(ns);
                string response = sr.ReadLine();

                //Console.WriteLine("Server response: " + response);
                MessageBox.Show(response, "", MessageBoxButton.OK);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // закриваємо підключення
                client.Close();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
