//using System.Text;
using System.Windows;
using System.Windows.Input;
using FlightTicketManager.GUI;
// using FlightTicketManager.Models; // Mở dòng này ra nếu bạn đã cấu hình xong Database

namespace FlightTicketManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnHeaderLogin_Click(object sender, MouseButtonEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnHeaderRegister_Click(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}