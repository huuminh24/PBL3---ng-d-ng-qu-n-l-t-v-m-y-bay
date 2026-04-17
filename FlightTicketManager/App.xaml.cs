using System;
using System.Windows;
using FlightTicketManager.GUI;

namespace FlightTicketManager
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                MainStaffWindow window = new MainStaffWindow();
                this.MainWindow = window;
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Lỗi runtime khi mở MainStaffWindow",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}