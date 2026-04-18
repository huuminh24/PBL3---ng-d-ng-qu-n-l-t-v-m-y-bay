using FlightTicketManager.BLL;
using FlightTicketManager.DTO;
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
    /// Interaction logic for FlightSearchWindow.xaml
    /// </summary>
    public partial class FlightSearchWindow : Window
    {
        private readonly FlightBLL _flightBLL = new FlightBLL();

        public FlightSearchWindow()
        {
            InitializeComponent();
            Loaded += FlightSearchWindow_Loaded;
        }

        private void FlightSearchWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dpDeparture.SelectedDate = DateTime.Today;
        }

        private void btnSearchFlight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string from = NormalizeAirportCode(txtOrigin.Text);
                string to = NormalizeAirportCode(txtDestination.Text);
                DateTime? departureDate = dpDeparture.SelectedDate;
                int passengerCount = GetPassengerCount();

                List<FlightDTO> flights = _flightBLL.GetFlights(from, to, departureDate, passengerCount);

                dgFlights.ItemsSource = flights;

                if (flights == null || flights.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy chuyến bay phù hợp!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi tìm chuyến bay",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnSelectFlight_Click(object sender, RoutedEventArgs e)
        {
            if (dgFlights.SelectedItem is not FlightDTO selectedFlight)
            {
                MessageBox.Show("Vui lòng chọn 1 chuyến bay trong danh sách!");
                return;
            }

            FrmSelectSeat seatWindow = new FrmSelectSeat(
             selectedFlight.FlightID,
             selectedFlight.EconomyPrice,
             selectedFlight.BusinessPrice
             );
            bool? seatResult = seatWindow.ShowDialog();

            if (seatResult == true && seatWindow.SelectedSeatId > 0)
            {
                decimal selectedPrice = seatWindow.SelectedSeatClass.Equals("Business", StringComparison.OrdinalIgnoreCase)
                ? selectedFlight.BusinessPrice
                : selectedFlight.EconomyPrice;

                FrmBookingInfo bookingWindow = new FrmBookingInfo(
                    selectedFlight.FlightID,
                    seatWindow.SelectedSeatId,
                    seatWindow.SelectedSeatNumber,
                    seatWindow.SelectedSeatClass,
                    selectedPrice
                );

                bookingWindow.ShowDialog();
            }
        }

        private string NormalizeAirportCode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";

            return input.Trim().ToUpper();
        }

        private int GetPassengerCount()
        {
            if (cbPassengers.SelectedItem is ComboBoxItem item)
            {
                string content = item.Content?.ToString() ?? "";

                if (content.StartsWith("2"))
                    return 2;

                return 1;
            }

            return 1;
        }
    }
}
