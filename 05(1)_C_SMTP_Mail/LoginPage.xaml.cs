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
        SmtpClient client;
        string? username;
        string? password;

        ClientPage clientPage;
        public LoginPage(ClientPage clientPage)
        {
            InitializeComponent();
            client = new SmtpClient();
            this.clientPage = clientPage;
        }

        public LoginPage() 
        {
            InitializeComponent();
        }

        //   tmvlad33@gmail.com
        //   gxknljmktrlthlyx
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            username = LoginTBox.Text;
            password = PasswordTBox.Text;

            client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

            try
            {
                client.Authenticate(username, password);
            }
            catch (AuthenticationException AunthEx)
            {
                MessageBox.Show($"Authentication failed. Check login and password: {AunthEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //_MyFrame.NavigationService.Navigate(new ClientPage());

            client.Disconnect(true);
            //LoginButton.IsEnabled = false;
            //LogOutButton.IsEnabled = true;
        }

        //private void Exit_Click(object sender, RoutedEventArgs e)
        //{
        //    //LoginButton.IsEnabled = false;
        //    //this.Close();
        //}

        //private void LogOut_Click(object sender, RoutedEventArgs e)
        //{
        //    //LoginButton.IsEnabled = true;
        //}
    }
}
