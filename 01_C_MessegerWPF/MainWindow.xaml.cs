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
        Socket socket;
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            //port = int.Parse(PortTBox.Text);
            //address = IpTBox.Text;
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                EndPoint remoteIpPoint = new IPEndPoint(IPAddress.Any, 0);

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                string message = "";
                while (true)
                {
                    // Message :
                    Console.Write("Enter a message: ");
                    message = MessageTBox.Text;
                    MessageTBox.Text = string.Empty;

                    if (message != string.Empty)
                    {
                        byte[] data = Encoding.Unicode.GetBytes(message);

                        MessagesLBox.Items.Add("You : ");
                        MessagesLBox.Items.Add(message);
                        MessagesLBox.Items.Add("");

                        // send :
                        socket.SendTo(data, ipPoint);

                        // Receive answer :
                        int bytes = 0;
                        string response = "";
                        data = new byte[1024]; // 1KB
                        do
                        {
                            bytes = socket.ReceiveFrom(data, ref remoteIpPoint);
                            response += Encoding.Unicode.GetString(data, 0, bytes);
                        } while (socket.Available > 0);

                        MessagesLBox.Items.Add("Chat AI : ");
                        MessagesLBox.Items.Add(response);
                        MessagesLBox.Items.Add("");
                    }
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
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            this.Close();
        }
    }
}
