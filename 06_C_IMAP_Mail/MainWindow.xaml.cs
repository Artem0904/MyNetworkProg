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
using System.Net.Sockets;

namespace _06_C_IMAP_Mail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string username = "artemshadiuk@gmail.com"; // change here
        const string password = "awlmhimwjozczybc"; // change here

        private ImapClient client = new();
        private List<IMailFolder> folders = new List<IMailFolder>();
        private List<MimeMessage> Messages = new List<MimeMessage>();

        string MessageText = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

            client.Authenticate(username, password);

            folders = client.GetFolders(client.PersonalNamespaces[0]).Where(x => x.Name != "[Gmail]").ToList();
           
            foreach (var fl in folders)
            {
                folderList.Items.Add(fl.Name);
            }
        }

        private async void CheckSubjects_DClick(object sender, MouseButtonEventArgs e)
        {
            if(subjectsList.Items.Count != 0)
            {
                subjectsList.Items.Clear();
            }
            try
            {
                int FolderNum = folderList.Items.IndexOf(folderList.SelectedItem);
                IMailFolder? folder = null;
                MailKit.SpecialFolder SpFolder = MailKit.SpecialFolder.All;
                switch (FolderNum)
                {
                    case 0:
                        break;
                    case 1:
                        SpFolder = MailKit.SpecialFolder.Flagged;
                        break;
                    case 2:
                        SpFolder = MailKit.SpecialFolder.Important;
                        break;
                    case 3:
                        SpFolder = MailKit.SpecialFolder.Trash;
                        break;
                    case 4:
                        SpFolder = MailKit.SpecialFolder.Sent;
                        break;
                    case 5:
                        SpFolder = MailKit.SpecialFolder.Junk;
                        break;
                    case 6:
                        SpFolder = MailKit.SpecialFolder.All;
                        break;
                    case 7:
                        SpFolder = MailKit.SpecialFolder.Drafts;
                        break;
                    default:
                        MessageBox.Show("Some error! Try again!");
                        return;
                }

                IList<MailKit.UniqueId> uids;
                if(FolderNum == 0)
                {
                    await client.Inbox.OpenAsync(FolderAccess.ReadOnly);
                    uids = await client.Inbox.SearchAsync(SearchQuery.All);
                    foreach (var uid in uids)
                    {
                        var message = await client.Inbox.GetMessageAsync(uid);
                        Messages.Add(message);
                        subjectsList.Items.Add(message.Subject);
                    }
                    return;
                }
                else
                {
                    folder = client.GetFolder(SpFolder);
                    await folder.OpenAsync(MailKit.FolderAccess.ReadOnly);
                    uids = await folder.SearchAsync(SearchQuery.All);
                }

                foreach (var i in uids)
                {
                    MimeMessage message = await folder.GetMessageAsync(i);
                    Messages.Add(message);
                    subjectsList.Items.Add(message.Subject);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CheckMessages_DClick(object sender, MouseButtonEventArgs e)
        {
            int SubjectNum = subjectsList.Items.IndexOf(subjectsList.SelectedItem);

            _MyFrame.NavigationService.Navigate(new MessagePage(Messages[SubjectNum]));
        }
    }
}
