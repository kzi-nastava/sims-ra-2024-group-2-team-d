using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.View.Owner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookingApp.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationReviewForm.xaml
    /// </summary>
    public partial class AccommodationReviewForm : Window, INotifyPropertyChanged
    {
        private readonly Reservation _reservation;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationReviewRepository _accommodationReviewRepository;
        private readonly ReservationRepository _reservationRepository;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Cleanness { get; set; }
        public int Rules { get; set; }
        public string Comment { get; set; }
        public string ImagePath { get; set; }

        public List<string> Images { get; set; }
        public AccommodationReviewForm(Reservation reservation)
        {
            InitializeComponent();
            DataContext = this;
            _reservation = reservation;
            _accommodationRepository = new AccommodationRepository();
            _accommodationReviewRepository = new AccommodationReviewRepository();
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
            AccommodationReview accommodationReview = new AccommodationReview(_reservation.Id, _reservation.AccomodationId, _reservation.UserId, Cleanness, Rules, Comment, Images);
            _accommodationReviewRepository.Save(accommodationReview);
            _reservation.ReviewedByGuest = true;
            _reservationRepository.Update(_reservation);
            MessageBox.Show("Uspjesno ste ocenili smestah=j");
            this.Close();

        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            if (Images is null)
            {
                Images = new System.Collections.Generic.List<string>();
            }
            Images.Add(ImagePath);
            ImagePath = String.Empty;
            OnPropertyChanged("ImagePath");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RenovationRecommendationForm renovationWindow = new RenovationRecommendationForm(_reservation);
            renovationWindow.Show();
        }
    }
}
