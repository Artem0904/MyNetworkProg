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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       // LoginPage loginPage;
        public MainWindow()
        {
            InitializeComponent();
            _MyFrame.NavigationService.Navigate(new LoginPage());
            //loginPage = new LoginPage();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
