using System;
using System.Windows;

namespace BookingApp.View.Guest1
{
    /// <summary>
    /// Interaction logic for ReservationConfirmation.xaml
    /// </summary>
    public partial class ReservationConfirmation : Window
    {
        public int NumberOfGuests { get; set; }
        public int MaxAccommodationGuestsNumber { get; set; }
        public ReservationConfirmation(int maxAccommodationGuestsNumber)
        {
            InitializeComponent();
            MaxAccommodationGuestsNumber = maxAccommodationGuestsNumber;
        }

        private void ReserveButtonClick(object sender, RoutedEventArgs e)
        {
            NumberOfGuests = Convert.ToInt32(Input1.Text);
            if (NumberOfGuests > MaxAccommodationGuestsNumber)
            {
                MessageBox.Show("Broj gostiju mora biti manji ili jednak " + MaxAccommodationGuestsNumber.ToString());
            }
            else
            {
                this.Close();
            }
        }
    }
}
