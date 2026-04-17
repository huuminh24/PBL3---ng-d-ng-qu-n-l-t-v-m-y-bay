using System.Windows;
using System.Windows.Controls;

namespace FlightTicketManager.GUI
{
    public partial class DatVeHoStaffWindow : Window
    {
        private readonly ChuyenBayDemo _chuyenBay;

        public VeDemo VeDaTao { get; private set; }

        public DatVeHoStaffWindow(ChuyenBayDemo chuyenBay, string maVe)
        {
            InitializeComponent();
            _chuyenBay = chuyenBay;

            TxtMaVe.Text = maVe;
            TxtMaChuyenBay.Text = chuyenBay.MaChuyenBay;
            TxtLoTrinh.Text = chuyenBay.NoiDi + " → " + chuyenBay.NoiDen + " | " + chuyenBay.GioKhoiHanh;
        }

        private void BtnXacNhan_Click(object sender, RoutedEventArgs e)
        {
            string tenKhachHang = TxtTenKhachHang.Text.Trim();
            string soGiayTo = TxtSoGiayTo.Text.Trim();
            string soGhe = TxtSoGhe.Text.Trim();
            string giaVeText = TxtGiaVe.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenKhachHang))
            {
                MessageBox.Show("Vui lòng nhập họ tên khách hàng.",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                TxtTenKhachHang.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(soGiayTo))
            {
                MessageBox.Show("Vui lòng nhập số giấy tờ.",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                TxtSoGiayTo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(soGhe))
            {
                MessageBox.Show("Vui lòng nhập số ghế.",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                TxtSoGhe.Focus();
                return;
            }

            decimal giaVe;
            if (!decimal.TryParse(giaVeText, out giaVe) || giaVe <= 0)
            {
                MessageBox.Show("Giá vé không hợp lệ.",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                TxtGiaVe.Focus();
                return;
            }

            string loaiHanhKhach = ((ComboBoxItem)CboLoaiHanhKhach.SelectedItem).Content.ToString();
            string hangGhe = ((ComboBoxItem)CboHangGhe.SelectedItem).Content.ToString();
            string thanhToan = ((ComboBoxItem)CboThanhToan.SelectedItem).Content.ToString();

            VeDaTao = new VeDemo
            {
                MaVe = TxtMaVe.Text,
                TenKhachHang = tenKhachHang,
                LoaiHanhKhach = loaiHanhKhach,
                SoGiayTo = soGiayTo,
                MaChuyenBay = _chuyenBay.MaChuyenBay,
                HangGhe = hangGhe,
                SoGhe = soGhe,
                GiaVe = giaVe,
                PhuongThucThanhToan = thanhToan,
                TrangThai = "Đã thanh toán"
            };

            DialogResult = true;
            Close();
        }

        private void BtnDong_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}