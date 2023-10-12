using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Win32;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
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
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace _06_C_IMAP_Mail
{
    /// <summary>
    /// Interaction logic for MessagePage.xaml
    /// </summary>
    public partial class MessagePage : Page
    {
        readonly string ClientUsername = ""; //user name
        readonly string ClientPassword = ""; // user pass

        MimeMessage MessageFrom;
        MimeMessage YourMessage;
        BodyBuilder builder = new BodyBuilder();
        public MessagePage(MimeMessage Message)
        {
            InitializeComponent();
            try
            {
                MessageFrom = Message;
                MessageBodyTxtBox.Text = $"\nDate : {MessageFrom.Date}\n From : {MessageFrom.From}\n To : {MessageFrom.To}\n\n <Subject : {MessageFrom.Subject}>\n Body : {MessageFrom.TextBody.ToString()}";
                foreach (var item in MessageFrom.Attachments)
                {
                    FileNamesLBox.Items.Add(item.ContentType.Name);
                }

            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if(toTxtBox.Text == string.Empty && subjectTxtBox.Text == string.Empty && bodyTxtBox.Text == string.Empty)
            {
                MessageBox.Show("Fields are empty!");
                return;
            }
            try
            {
                YourMessage = new MimeMessage();
                YourMessage.From.Add(new MailboxAddress("Artem", ClientUsername));
                YourMessage.To.Add(new MailboxAddress("Person", toTxtBox.Text));
                YourMessage.Subject = subjectTxtBox.Text;
                YourMessage.Importance = MessageImportance.High;

                    builder.TextBody = bodyTxtBox.Text;
              
                    YourMessage.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

                    client.Authenticate(ClientUsername, ClientPassword);
                
                    client.Send(YourMessage);
                }
                toTxtBox.Text = string.Empty;
                subjectTxtBox.Text = string.Empty;
                bodyTxtBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                toTxtBox.Text = string.Empty;
                subjectTxtBox.Text = string.Empty;
                bodyTxtBox.Text = string.Empty;
                MessageBox.Show(ex.Message);
            }
        }

        private void Atachments_Click(object sender, RoutedEventArgs e)
        {
            if (toTxtBox.Text == string.Empty && subjectTxtBox.Text == string.Empty && bodyTxtBox.Text == string.Empty)
            {
                MessageBox.Show("Field are empty!");
                return;
            }
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == true)
                {
                    builder.Attachments.Add(dialog.FileName);

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
