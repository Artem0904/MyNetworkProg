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
using static System.Environment;
using System.Xml;
using MailKit.Net.Imap;
using MimeKit;
using MailKit.Search;
using MailKit.Security;
using MailKit;

namespace _06_C_IMAP_Mail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string username = "tmvlad33@gmail.com"; // change here
        const string password = "gxknljmktrlthlyx"; // change here

        private ImapClient client = new();
        private IList<IMailFolder> folders;
        public MainWindow()
        {
            InitializeComponent();
            client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

            client.Authenticate(username, password);

            folders = client.GetFolders(client.PersonalNamespaces[0]); // зробити вибірку без ГМАІЛ папки
            foreach (var fl in folders)
            {
                if (fl.Name == "[Gmail]")
                    //folders.Remove(fl); //видалятииииииииииииииииии
                folderList.Items.Add(fl.Name);
            }
        }

        private void CheckMessages_DClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int a = folderList.Items.IndexOf(folderList.SelectedItem);



                var folder = client.GetFolder(MailKit.SpecialFolder.All);
                folder.Open(MailKit.FolderAccess.ReadOnly);

                //client.Inbox.Open(FolderAccess.ReadOnly);
                IList<MailKit.UniqueId> uids = folder.Search(SearchQuery.All);

                //if() name = inbox -^
                foreach (var i in uids)
                {
                    MimeMessage message = folder.GetMessage(i);
                    messagesList.Items.Add($"{message.Subject}");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
