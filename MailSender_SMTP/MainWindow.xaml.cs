using System.Windows;

namespace MailSender_SMTP
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string gmail = gmailTextBox.Text;
            string password = passwordBox.Password;
            if (string.IsNullOrEmpty(gmail))
            {
                MessageBox.Show("Enter valid <gmail>");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Enter valid <password>");
                return;
            }
            var win = new SendWin(gmail, password);
            win.Show();
            this.Close();
        }
    }
}