using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FlightTicketManager.GUI
{
    public partial class ThongKeDoanhThuStaffPage : Page
    {
        private List<ThongKeNgayDemo> _dsThongKe = new List<ThongKeNgayDemo>();

        public ThongKeDoanhThuStaffPage()
        {
            InitializeComponent();
            TaiThongKeDemo();
        }

        private void BtnTaiThongKeDemo_Click(object sender, RoutedEventArgs e)
        {
            TaiThongKeDemo();
            MessageBox.Show("Đã tải thống kê doanh thu demo.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TaiThongKeDemo()
        {
            _dsThongKe = new List<ThongKeNgayDemo>
            {
                new ThongKeNgayDemo
                {
                    Ngay = "14/04/2026",
                    SoVeDaBan = 12,
                    SoVeDaHuy = 1,
                    DoanhThu = 18600000,
                    YeuCauChoXuLy = 2,
                    GhiChu = "Lượng khách ổn định"
                },
                new ThongKeNgayDemo
                {
                    Ngay = "15/04/2026",
                    SoVeDaBan = 18,
                    SoVeDaHuy = 2,
                    DoanhThu = 27450000,
                    YeuCauChoXuLy = 1,
                    GhiChu = "Tăng mạnh tuyến Đà Nẵng - TP.HCM"
                },
                new ThongKeNgayDemo
                {
                    Ngay = "16/04/2026",
                    SoVeDaBan = 15,
                    SoVeDaHuy = 1,
                    DoanhThu = 22300000,
                    YeuCauChoXuLy = 3,
                    GhiChu = "Có nhiều yêu cầu đổi chuyến bay"
                },
                new ThongKeNgayDemo
                {
                    Ngay = "17/04/2026",
                    SoVeDaBan = 20,
                    SoVeDaHuy = 2,
                    DoanhThu = 31500000,
                    YeuCauChoXuLy = 2,
                    GhiChu = "Cuối tuần lượng đặt vé tăng"
                }
            };

            DgThongKe.ItemsSource = null;
            DgThongKe.ItemsSource = _dsThongKe;

            TxtTongVeDaBan.Text = _dsThongKe.Sum(x => x.SoVeDaBan).ToString();
            TxtVeDaHuy.Text = _dsThongKe.Sum(x => x.SoVeDaHuy).ToString();
            TxtTongDoanhThu.Text = _dsThongKe.Sum(x => x.DoanhThu).ToString("N0") + " VNĐ";
            TxtYeuCauChoXuLy.Text = _dsThongKe.Sum(x => x.YeuCauChoXuLy).ToString();
        }
    }

    public class ThongKeNgayDemo
    {
        public string Ngay { get; set; }
        public int SoVeDaBan { get; set; }
        public int SoVeDaHuy { get; set; }
        public decimal DoanhThu { get; set; }
        public int YeuCauChoXuLy { get; set; }
        public string GhiChu { get; set; }

        public string DoanhThuHienThi
        {
            get { return DoanhThu.ToString("N0") + " VNĐ"; }
        }
    }
}