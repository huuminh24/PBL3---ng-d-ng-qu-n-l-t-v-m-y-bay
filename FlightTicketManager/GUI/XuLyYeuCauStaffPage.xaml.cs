using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace FlightTicketManager.GUI
{
    public partial class XuLyYeuCauStaffPage : Page
    {
        private ObservableCollection<YeuCauDemo> _dsYeuCau = new ObservableCollection<YeuCauDemo>();
        private int _soYeuCauTuTang = 4;

        public XuLyYeuCauStaffPage()
        {
            InitializeComponent();
            DgYeuCau.ItemsSource = _dsYeuCau;
            TaiDuLieuDemo();
        }

        private void BtnTaiDuLieuDemo_Click(object sender, RoutedEventArgs e)
        {
            TaiDuLieuDemo();
            MessageBox.Show("Đã tải dữ liệu yêu cầu demo.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TaiDuLieuDemo()
        {
            _dsYeuCau.Clear();

            _dsYeuCau.Add(new YeuCauDemo
            {
                MaYeuCau = "YC001",
                MaVe = "VE000",
                LoaiYeuCau = "Hủy vé",
                LyDo = "Khách đổi lịch công tác",
                TrangThai = "Đã ghi nhận"
            });

            _dsYeuCau.Add(new YeuCauDemo
            {
                MaYeuCau = "YC002",
                MaVe = "VE001",
                LoaiYeuCau = "Cập nhật thông tin",
                LyDo = "Sai số CCCD của hành khách",
                TrangThai = "Đã ghi nhận"
            });

            _dsYeuCau.Add(new YeuCauDemo
            {
                MaYeuCau = "YC003",
                MaVe = "VE002",
                LoaiYeuCau = "Đổi chuyến bay",
                LyDo = "Khách muốn bay chuyến muộn hơn",
                TrangThai = "Đã ghi nhận"
            });

            _soYeuCauTuTang = 4;
        }

        private void BtnGhiNhanYeuCau_Click(object sender, RoutedEventArgs e)
        {
            string maVe = TxtMaVeYeuCau.Text.Trim();
            string lyDo = TxtLyDoYeuCau.Text.Trim();
            ComboBoxItem? selectedItem = CboLoaiYeuCau.SelectedItem as ComboBoxItem;
            string loaiYeuCau = selectedItem != null ? selectedItem.Content.ToString() ?? "Khác" : "Khác";

            if (string.IsNullOrWhiteSpace(maVe))
            {
                MessageBox.Show("Vui lòng nhập mã vé.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxtMaVeYeuCau.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(lyDo))
            {
                MessageBox.Show("Vui lòng nhập lý do yêu cầu.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxtLyDoYeuCau.Focus();
                return;
            }

            YeuCauDemo yeuCauMoi = new YeuCauDemo
            {
                MaYeuCau = $"YC{_soYeuCauTuTang:000}",
                MaVe = maVe,
                LoaiYeuCau = loaiYeuCau,
                LyDo = lyDo,
                TrangThai = "Đã ghi nhận"
            };

            _dsYeuCau.Add(yeuCauMoi);
            _soYeuCauTuTang++;

            TxtMaVeYeuCau.Clear();
            TxtLyDoYeuCau.Clear();
            CboLoaiYeuCau.SelectedIndex = 0;

            MessageBox.Show($"Đã ghi nhận yêu cầu {yeuCauMoi.MaYeuCau}.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}