using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for GuestReviewForm.xaml
    /// </summary>
    public partial class GuestReviewForm : Window
    {
        private readonly Reservation _reservation;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly GuestReviewRepository _guestReviewRepository;
        private readonly ReservationRepository _reservationRepository;

        public int Cleanness { get; set; }
        public int Rules { get; set; }
        public string Comment{ get; set; }
        public GuestReviewForm(Reservation reservation)
        {
            InitializeComponent();
            DataContext = this;
            _reservation = reservation;
            _accommodationRepository = new AccommodationRepository();
            _guestReviewRepository = new GuestReviewRepository();
            _reservationRepository = new ReservationRepository();
            Information.Text += " " + _accommodationRepository.GetById(reservation.AccomodationId).Name + 
                                " smjestaju u periodu izmedju " + reservation.ReservationDateRange.StartDate.ToString() + 
                                " i " + reservation.ReservationDateRange.EndDate.ToString();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            GuestReview guestReview = new GuestReview(_reservation.AccomodationId, _reservation.UserId, Cleanness, Rules, Comment);
            _guestReviewRepository.Save(guestReview);
            _reservation.ReviewedByOwner = true;
            _reservationRepository.Update(_reservation);
            MessageBox.Show("Uspjesno ste ocenili gosta");
            this.Close();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
