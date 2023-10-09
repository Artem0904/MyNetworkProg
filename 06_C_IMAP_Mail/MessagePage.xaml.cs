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

namespace _06_C_IMAP_Mail
{
    /// <summary>
    /// Interaction logic for MessagePage.xaml
    /// </summary>
    public partial class MessagePage : Page
    {
        string ClientUsername = "artemshadiuk@gmail.com";
        string ClientPassword = "awlmhimwjozczybc";

        MimeMessage Message;
        public MessagePage(MimeMessage Message)
        {
            InitializeComponent();
            this.Message = Message;
            MessageBodyTxtBox.Text = Message.TextBody.ToString();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Artem", ClientUsername));
            message.To.Add(new MailboxAddress("Person", toTxtBox.Text));
            message.Subject = subjectTxtBox.Text;
            message.Importance = MessageImportance.High;

            message.Body = new TextPart()
            {
                Text = bodyTxtBox.Text
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

                client.Authenticate(ClientUsername, ClientPassword);

                client.Send(message);
            }
        }

        private void Atachments_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
