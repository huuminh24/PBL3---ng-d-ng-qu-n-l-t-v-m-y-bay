using FlightTicketManager.BLL;
using FlightTicketManager.DTO;
using FlightTicketManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightTicketManager.GUI
{
    /// <summary>
    /// Interaction logic for FrmSelectSeat.xaml
    /// </summary>
    public partial class FrmSelectSeat : Window
    {
        private int _flightId;
        private decimal _economyPrice;
        private decimal _businessPrice;

        private readonly SeatBLL _seatBLL = new SeatBLL();
        private Button? _currentSelectedButton;

        public int SelectedSeatId { get; private set; } = -1;
        public string SelectedSeatNumber { get; private set; } = "";
        public string SelectedSeatClass { get; private set; } = "";

        public FrmSelectSeat(int flightId, decimal economyPrice, decimal businessPrice)
        {
            InitializeComponent();
            _flightId = flightId;
            _economyPrice = economyPrice;
            _businessPrice = businessPrice;
            Loaded += FrmSelectSeat_Loaded;
        }

        private void FrmSelectSeat_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSeats();
        }

        private void LoadSeats()
        {
            ugSeats.Children.Clear();

            List<SeatDTO> seats = _seatBLL.GetSeatsByFlight(_flightId);

            if (seats == null || seats.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu ghế cho chuyến bay này!");
                return;
            }

            foreach (SeatDTO seat in seats)
            {
                Button btnSeat = new Button
                {
                    Width = 80,
                    Height = 45,
                    Margin = new Thickness(8),
                    Content = seat.SeatNumber,
                    Tag = seat,
                    FontWeight = FontWeights.SemiBold,
                    BorderThickness = new Thickness(0),
                    Foreground = Brushes.White
                };

                if (seat.SeatStatus == "Available")
                {
                    btnSeat.Background = Brushes.Green;
                    btnSeat.Click += SeatButton_Click;
                }
                else
                {
                    btnSeat.Background = Brushes.Gray;
                    btnSeat.IsEnabled = false;
                }

                ugSeats.Children.Add(btnSeat);
            }
        }

        private void SeatButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            SeatDTO seat = btn.Tag as SeatDTO;
            if (seat == null) return;

            if (_currentSelectedButton != null)
                _currentSelectedButton.Background = Brushes.Green;

            _currentSelectedButton = btn;
            _currentSelectedButton.Background = Brushes.Orange;

            SelectedSeatId = seat.SeatID;
            SelectedSeatNumber = seat.SeatNumber;
            SelectedSeatClass = seat.SeatClass;

            txtSelectedSeat.Text = seat.SeatNumber;
            txtSeatClass.Text = seat.SeatClass;
            txtSeatStatus.Text = seat.SeatStatus;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSeatId <= 0)
            {
                MessageBox.Show("Vui lòng chọn ghế trước khi tiếp tục!");
                return;
            }

            decimal selectedPrice = SelectedSeatClass.Equals("Business", StringComparison.OrdinalIgnoreCase)
                ? _businessPrice
                : _economyPrice;

            FrmBookingInfo bookingWindow = new FrmBookingInfo(
                _flightId,
                SelectedSeatId,
                SelectedSeatNumber,
                SelectedSeatClass,
                selectedPrice
            );

            bookingWindow.ShowDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
