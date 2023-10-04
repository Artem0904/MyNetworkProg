using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace _01_C_MessegerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int port = 8080;
        static string address = "127.0.0.1";
        public MainWindow()
        {
            InitializeComponent();

        }
        UdpClient client;
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if(PortTBox.Text != string.Empty && IpTBox.Text != string.Empty)
            {
                port = int.Parse(PortTBox.Text);
                address = IpTBox.Text;
            }

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            IPEndPoint remoteIpPoint = new IPEndPoint(IPAddress.Any, 0);

            client = new UdpClient();
            try
            {

                string message = string.Empty;
                    // Message :
                    message = MessageTBox.Text;
                    MessageTBox.Text = string.Empty;

                    if (message != string.Empty)
                    {
                        byte[] data = Encoding.Unicode.GetBytes(message);

                        MessagesLBox.Items.Add("You : ");
                        MessagesLBox.Items.Add(message);
                        MessagesLBox.Items.Add("");

                        // send :
                        client.Send(data, data.Length, ipPoint);

                        // Receive answer :
                        data = client.Receive(ref remoteIpPoint);
                        string response = Encoding.Unicode.GetString(data);

                        MessagesLBox.Items.Add("Chat AI : ");
                        MessagesLBox.Items.Add(response);
                        MessagesLBox.Items.Add("");

                    }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EndChat_Click(object sender, RoutedEventArgs e)
        {
            // Close socket
            client.Close();
            this.Close();
        }
    }
}
