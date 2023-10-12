using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Win32;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public ClientPage(string ClientUsername, string ClientPassword)
        {
            InitializeComponent();
            this.ClientUsername = ClientUsername;
            this.ClientPassword = ClientPassword;
        }
        BodyBuilder builder = new BodyBuilder();
        MimeMessage YourMessage;

        private async void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                YourMessage = new MimeMessage();
                YourMessage.From.Add(new MailboxAddress("Artem", ClientUsername));
                YourMessage.To.Add(new MailboxAddress("Person", ToTBox.Text));
                YourMessage.Subject = SubjectTBox.Text;

                MessageImportance messageImportance = new MessageImportance();
                if (LowRButton.IsFocused)
                {
                    messageImportance = MessageImportance.Low;
                }
                else if (HighRButton.IsFocused)
                {
                    messageImportance = MessageImportance.High;
                }
                else
                    messageImportance = MessageImportance.Normal;

                YourMessage.Importance = messageImportance;

                builder.TextBody = MessageTBox.Text;

                YourMessage.Body = builder.ToMessageBody();

                if (ToTBox.Text == string.Empty && SubjectTBox.Text == string.Empty && MessageTBox.Text == string.Empty)
                {
                    MessageBox.Show("Fields are empty!");
                    return;
                }

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

                    await client.AuthenticateAsync(ClientUsername, ClientPassword);

                    await client.SendAsync(YourMessage);
                }
                ToTBox.Text = string.Empty;
                SubjectTBox.Text = string.Empty;
                MessageTBox.Text = string.Empty;
                NormalRButton.IsChecked = true;
                builder = new BodyBuilder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Atachments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == true)
                {
                    builder.Attachments.Add(dialog.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
