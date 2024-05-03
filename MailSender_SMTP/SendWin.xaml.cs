using Microsoft.Win32;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace MailSender_SMTP
{
    public partial class SendWin : Window
    {

        string server = "smtp.gmail.com";
        int port = 587;

        string gmail;
        string password;
        //oliaopanasiukk@gmail.com
        //yqne vhnx qmgw qgvf
        //kecaxi9205@goulink.com
        MailMessage? message = null;

        public SendWin(string gmail, string password)
        {
            InitializeComponent();
            this.gmail = gmail;
            this.password = password;
            fromObj.Content = gmail;
            message = new MailMessage();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(toObj.Text))
            {
                MessageBox.Show("Please, enter the recipient");
                return;
            }
            if (string.IsNullOrWhiteSpace(body.Text))
            {
                var a = MessageBox.Show("Do you really want to send a blank email?", "Notification", MessageBoxButton.YesNoCancel);
                if (a == MessageBoxResult.No || a == MessageBoxResult.Cancel) return;
            }

            message.From = new MailAddress(gmail);
            message.To.Add(toObj.Text);
            message.Subject = subject.Text;
            message.Body = body.Text;

            SmtpClient client = new SmtpClient(server, port);
            client.Credentials = new NetworkCredential(gmail, password);

            if (checkBox.IsChecked == true)
            {
                message.Priority = MailPriority.High;
            }

            client.EnableSsl = true;
            client.SendCompleted += Client_SendCompleted;

            try
            {
                client.SendAsync(message, message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }
            if (e.Cancelled)
            {
                MessageBox.Show($"Message wasn`t sent");
            }
            MessageBox.Show($"Message was send successfully!");
            toObj.Text = "";    
            subject.Text = "";
            body.Text = "";
            checkBox.IsChecked = false;
            listBox.Items.Clear();
        }

        private void AttachFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                foreach (string file in dialog.FileNames)
                    message.Attachments.Add(new Attachment(file));

                foreach (string fileName in dialog.SafeFileNames)
                    listBox.Items.Add("> " + fileName);
            }
        }
    }
}
