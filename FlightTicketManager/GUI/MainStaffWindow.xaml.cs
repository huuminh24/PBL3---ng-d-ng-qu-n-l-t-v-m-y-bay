using System.Windows;
using System.Windows.Media;

namespace FlightTicketManager.GUI
{
    public partial class MainStaffWindow : Window
    {
        private readonly Brush _activeBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E88E5"));
        private readonly Brush _inactiveBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEF2F7"));
        private readonly Brush _activeForeground = Brushes.White;
        private readonly Brush _inactiveForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E293B"));

        public MainStaffWindow()
        {
            InitializeComponent();
            HienThiTrangTraCuuVe();
        }

        private void BtnTraCuuVe_Click(object sender, RoutedEventArgs e)
        {
            HienThiTrangTraCuuVe();
        }

        private void BtnThongKeDoanhThu_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new ThongKeDoanhThuStaffPage());
            CapNhatTrangThaiMenu(true);
        }

        private void BtnDangXuat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HienThiTrangTraCuuVe()
        {
            MainContentFrame.Navigate(new TraCuuVeStaffPage());
            CapNhatTrangThaiMenu(false);
        }

        private void CapNhatTrangThaiMenu(bool dangMoThongKe)
        {
            if (dangMoThongKe)
            {
                BtnMenuThongKe.Background = _activeBackground;
                BtnMenuThongKe.Foreground = _activeForeground;
                BtnMenuTraCuuVe.Background = _inactiveBackground;
                BtnMenuTraCuuVe.Foreground = _inactiveForeground;
            }
            else
            {
                BtnMenuTraCuuVe.Background = _activeBackground;
                BtnMenuTraCuuVe.Foreground = _activeForeground;
                BtnMenuThongKe.Background = _inactiveBackground;
                BtnMenuThongKe.Foreground = _inactiveForeground;
            }
        }
    }
}