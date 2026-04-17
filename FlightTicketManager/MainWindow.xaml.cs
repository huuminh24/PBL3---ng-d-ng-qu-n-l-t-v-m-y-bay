using System;
using System.Linq;
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