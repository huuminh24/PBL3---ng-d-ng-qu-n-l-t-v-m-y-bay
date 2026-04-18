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
    /// Interaction logic for FrmBookingInfo.xaml
    /// </summary>
    public partial class FrmBookingInfo : Window
    {
        private int _flightId;
        private int _seatId;
        private string _seatNumber = "";
        private string _seatClass = "";
        private decimal _selectedPrice;

        public FrmBookingInfo()
        {
            InitializeComponent();
        }

        public FrmBookingInfo(int flightId, int seatId, string seatNumber, string seatClass, decimal selectedPrice) : this()
        {
            _flightId = flightId;
            _seatId = seatId;
            _seatNumber = seatNumber;
            _seatClass = seatClass;
            _selectedPrice = selectedPrice;

            Loaded += FrmBookingInfo_Loaded;
        }

        private void FrmBookingInfo_Loaded(object sender, RoutedEventArgs e)
        {
            txtFlightId.Text = _flightId.ToString();
            txtSeatNumber.Text = _seatNumber;
            txtSeatClassInfo.Text = _seatClass;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPassengerName.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên hành khách!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDocumentNumber.Text))
                {
                    MessageBox.Show("Vui lòng nhập số giấy tờ!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtContactName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên người liên hệ!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Vui lòng nhập email liên hệ!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại liên hệ!");
                    return;
                }

                string passengerType = GetSelectedComboBoxText(cboPassengerType, "Adult");
                string documentType = GetSelectedComboBoxText(cboDocumentType, "CCCD");

                BookingRequestDTO request = new BookingRequestDTO
                {
                    CustomerProfileID = 2,
                    CreatedByProfileID = 2,
                    FlightID = _flightId,

                    ContactFullName = txtContactName.Text.Trim(),
                    ContactEmail = txtEmail.Text.Trim(),
                    ContactPhone = txtPhone.Text.Trim(),

                    OriginalAmount = _selectedPrice,
                    DiscountAmount = 0,
                    FinalAmount = _selectedPrice,

                    HoldExpiredAt = DateTime.Now.AddMinutes(30)
                };

                request.Passengers.Add(new PassengerDTO
                {
                    FullName = txtPassengerName.Text.Trim(),
                    PassengerType = passengerType,
                    DocumentType = documentType,
                    DocumentNumber = txtDocumentNumber.Text.Trim(),
                    SeatID = _seatId,
                    SeatClass = _seatClass
                });

                BookingBLL bookingBLL = new BookingBLL();
                string bookingCode = bookingBLL.CreateBooking(request);

                MessageBox.Show(
                    "Đặt vé thành công!\n\n" +
                    "Mã booking: " + bookingCode +
                    "\nGhế: " + _seatNumber +
                    "\nGiá vé: " + _selectedPrice.ToString("N0") + " VND" +
                    "\nGiữ chỗ đến: " + DateTime.Now.AddMinutes(30).ToString("dd/MM/yyyy HH:mm"),
                    "Thông báo"
                );

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu booking: " + ex.Message, "Lỗi");
            }
        }

        private string GetSelectedComboBoxText(ComboBox comboBox, string defaultValue)
        {
            if (comboBox.SelectedItem is ComboBoxItem item && item.Content != null)
                return item.Content.ToString() ?? defaultValue;

            return defaultValue;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
