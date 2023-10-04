using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
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
    public partial class ClientPage : Page
    {
        string ClientUsername;
        string ClientPassword;
        public ClientPage()
        {
            InitializeComponent();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {

            //ClientUsername = ;
            //ClientPassword = ;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Artem", ClientUsername));  
            message.To.Add(new MailboxAddress("Person", ToTBox.Text)); 
            message.Subject = SubjectTBox.Text;
            message.Importance = MessageImportance.High;

            message.Body = new TextPart()
            {
                Text = MessageTBox.Text
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

                client.Authenticate(ClientUsername, ClientPassword);

                client.Send(message);
            }
        }
    }
}
