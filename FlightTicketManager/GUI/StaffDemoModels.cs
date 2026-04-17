using System.ComponentModel;

namespace FlightTicketManager.GUI
{
    public class ChuyenBayDemo
    {
        public string MaChuyenBay { get; set; }
        public string NoiDi { get; set; }
        public string NoiDen { get; set; }
        public string GioKhoiHanh { get; set; }
    }

    public class VeDemo : INotifyPropertyChanged
    {
        private string _maVe;
        private string _tenKhachHang;
        private string _loaiHanhKhach;
        private string _soGiayTo;
        private string _maChuyenBay;
        private string _hangGhe;
        private string _soGhe;
        private decimal _giaVe;
        private string _phuongThucThanhToan;
        private string _trangThai;

        public string MaVe
        {
            get { return _maVe; }
            set { _maVe = value; OnPropertyChanged(nameof(MaVe)); }
        }

        public string TenKhachHang
        {
            get { return _tenKhachHang; }
            set { _tenKhachHang = value; OnPropertyChanged(nameof(TenKhachHang)); }
        }

        public string LoaiHanhKhach
        {
            get { return _loaiHanhKhach; }
            set { _loaiHanhKhach = value; OnPropertyChanged(nameof(LoaiHanhKhach)); }
        }

        public string SoGiayTo
        {
            get { return _soGiayTo; }
            set { _soGiayTo = value; OnPropertyChanged(nameof(SoGiayTo)); }
        }

        public string MaChuyenBay
        {
            get { return _maChuyenBay; }
            set { _maChuyenBay = value; OnPropertyChanged(nameof(MaChuyenBay)); }
        }

        public string HangGhe
        {
            get { return _hangGhe; }
            set { _hangGhe = value; OnPropertyChanged(nameof(HangGhe)); }
        }

        public string SoGhe
        {
            get { return _soGhe; }
            set { _soGhe = value; OnPropertyChanged(nameof(SoGhe)); }
        }

        public decimal GiaVe
        {
            get { return _giaVe; }
            set
            {
                _giaVe = value;
                OnPropertyChanged(nameof(GiaVe));
                OnPropertyChanged(nameof(GiaVeHienThi));
            }
        }

        public string PhuongThucThanhToan
        {
            get { return _phuongThucThanhToan; }
            set { _phuongThucThanhToan = value; OnPropertyChanged(nameof(PhuongThucThanhToan)); }
        }

        public string TrangThai
        {
            get { return _trangThai; }
            set { _trangThai = value; OnPropertyChanged(nameof(TrangThai)); }
        }

        public string GiaVeHienThi
        {
            get { return GiaVe.ToString("N0") + " VNĐ"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string tenThuocTinh)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(tenThuocTinh));
        }
    }

    public class YeuCauDemo : INotifyPropertyChanged
    {
        private string _maYeuCau;
        private string _maVe;
        private string _loaiYeuCau;
        private string _lyDo;
        private string _trangThai;

        public string MaYeuCau
        {
            get { return _maYeuCau; }
            set { _maYeuCau = value; OnPropertyChanged(nameof(MaYeuCau)); }
        }

        public string MaVe
        {
            get { return _maVe; }
            set { _maVe = value; OnPropertyChanged(nameof(MaVe)); }
        }

        public string LoaiYeuCau
        {
            get { return _loaiYeuCau; }
            set { _loaiYeuCau = value; OnPropertyChanged(nameof(LoaiYeuCau)); }
        }

        public string LyDo
        {
            get { return _lyDo; }
            set { _lyDo = value; OnPropertyChanged(nameof(LyDo)); }
        }

        public string TrangThai
        {
            get { return _trangThai; }
            set { _trangThai = value; OnPropertyChanged(nameof(TrangThai)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string tenThuocTinh)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(tenThuocTinh));
        }
    }
}
