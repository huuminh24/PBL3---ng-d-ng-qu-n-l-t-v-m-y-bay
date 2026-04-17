using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace FlightTicketManager.GUI
{
    public partial class TraCuuVeStaffPage : Page
    {
        private List<ChuyenBayDemo> _dsChuyenBay = new List<ChuyenBayDemo>();
        private ObservableCollection<VeDemo> _dsVe = new ObservableCollection<VeDemo>();
        private int _soVeTuTang = 1;

        public TraCuuVeStaffPage()
        {
            InitializeComponent();
            NapDanhSachVeDemoBanDau();
            DgVe.ItemsSource = _dsVe;
        }

        private void NapDanhSachVeDemoBanDau()
        {
            _dsVe.Clear();

            _dsVe.Add(new VeDemo
            {
                MaVe = "VE000",
                TenKhachHang = "Nguyễn Văn A",
                LoaiHanhKhach = "Người lớn",
                SoGiayTo = "012345678",
                MaChuyenBay = "VN101",
                HangGhe = "Economy",
                SoGhe = "A01",
                GiaVe = 1500000,
                PhuongThucThanhToan = "Tiền mặt",
                TrangThai = "Đã thanh toán"
            });

            _soVeTuTang = 1;
        }

        private void BtnTimChuyenBay_Click(object sender, RoutedEventArgs e)
        {
            string noiDi = ((CboNoiDi.SelectedItem as ComboBoxItem)?.Content?.ToString()) ?? "Đà Nẵng";
            string noiDen = ((CboNoiDen.SelectedItem as ComboBoxItem)?.Content?.ToString()) ?? "Hà Nội";

            _dsChuyenBay = new List<ChuyenBayDemo>
            {
                new ChuyenBayDemo
                {
                    MaChuyenBay = "VN101",
                    NoiDi = noiDi,
                    NoiDen = noiDen,
                    GioKhoiHanh = "20/04/2026 08:30"
                },
                new ChuyenBayDemo
                {
                    MaChuyenBay = "VJ202",
                    NoiDi = noiDi,
                    NoiDen = noiDen,
                    GioKhoiHanh = "20/04/2026 13:45"
                },
                new ChuyenBayDemo
                {
                    MaChuyenBay = "QH303",
                    NoiDi = noiDi,
                    NoiDen = noiDen,
                    GioKhoiHanh = "21/04/2026 09:15"
                }
            };

            DgChuyenBay.ItemsSource = null;
            DgChuyenBay.ItemsSource = _dsChuyenBay;
        }

        private void BtnDatVeHo_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ChuyenBayDemo chuyenBay = btn?.Tag as ChuyenBayDemo;

            if (chuyenBay == null)
            {
                MessageBox.Show("Không lấy được thông tin chuyến bay.",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            string maVeMoi = $"VE{_soVeTuTang:000}";

            DatVeHoStaffWindow datVeWindow = new DatVeHoStaffWindow(chuyenBay, maVeMoi);
            datVeWindow.Owner = Window.GetWindow(this);

            bool? ketQua = datVeWindow.ShowDialog();

            if (ketQua == true && datVeWindow.VeDaTao != null)
            {
                _dsVe.Add(datVeWindow.VeDaTao);
                _soVeTuTang++;

                MessageBox.Show($"Đặt vé hộ thành công!\nMã vé: {datVeWindow.VeDaTao.MaVe}",
                                "Thành công",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

        private void BtnHuyVe_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            VeDemo ve = btn?.Tag as VeDemo;

            if (ve == null)
            {
                MessageBox.Show("Không lấy được thông tin vé.",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            if (ve.TrangThai == "Đã hủy")
            {
                MessageBox.Show("Vé này đã bị hủy trước đó.",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                return;
            }

            MessageBoxResult luaChon = MessageBox.Show(
                $"Bạn có chắc muốn hủy vé {ve.MaVe} của khách {ve.TenKhachHang} không?",
                "Xác nhận hủy vé",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (luaChon == MessageBoxResult.Yes)
            {
                ve.TrangThai = "Đã hủy";
                DgVe.Items.Refresh();

                MessageBox.Show($"Đã hủy vé {ve.MaVe}",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }
    }
}