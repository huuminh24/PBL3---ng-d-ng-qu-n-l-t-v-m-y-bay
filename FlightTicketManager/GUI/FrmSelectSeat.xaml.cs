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
        public FrmSelectSeat(int flightId)
        {
            InitializeComponent();
            _flightId = flightId;
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
