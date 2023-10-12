using MailKit.Net.Smtp;
using MailKit.Security;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _05_1__C_SMTP_Mail
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        NavigationService nav;
        SmtpClient client = new SmtpClient();
        string? username;
        string? password;

        public MainWindow ParentWindow { get; set; }

        ClientPage clientPage;
        //public LoginPage(ClientPage clientPage, MainWindow parentWindow)
        //{
        //    InitializeComponent();
        //    ParentWindow = parentWindow;
        //    this.clientPage = clientPage;
        //}

        public LoginPage(MainWindow parentWindow)
        {
            InitializeComponent();
            ParentWindow = parentWindow;
        }

        //   artemshadiuk@gmail.com
        //   awlmhimwjozczybc
        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            username = LoginTBox.Text;
            password = PasswordTBox.Text;
            if (username == null || password == null)
            {
                MessageBox.Show("Fields are empty!");
                return;
            }
            await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

            try
            {
                await client.AuthenticateAsync(username, password);
            }
            catch (AuthenticationException AunthEx)
            {
                MessageBox.Show($"Authentication failed. Check login and password: {AunthEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            clientPage = new ClientPage(username, password);
            ParentWindow._MyFrame.NavigationService.Navigate(clientPage);
            MessageBox.Show("YES");
            await client.DisconnectAsync(true);
        }
    }
}
