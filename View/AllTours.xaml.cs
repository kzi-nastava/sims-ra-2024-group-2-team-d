using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AllTours.xaml
    /// </summary>
    public partial class AllTours : Window
    {
        public User LoggedInUser { get; set; }
        public TourInstance TourInstance { get; set; }
        private readonly TourInstanceRepository _instanceRepository;
        public static ObservableCollection<TourInstance> TourInstances { get; set; }

        private readonly TourRepository _tourRepository;

        public TourReservation TourReservation { get; set; }
        private readonly TourReservationRepository _reservationRepository;
        public static ObservableCollection<TourReservation> TourReservations { get; set; }


        public TourInstance SelectedTourInstance { get; set; }


        public AllTours(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _instanceRepository = new TourInstanceRepository();
            _tourRepository = new TourRepository();
            TourInstances = new ObservableCollection<TourInstance>(_instanceRepository.GetAll());
            LinkTourInstancesWithTours();
            TourInstances = new ObservableCollection<TourInstance>(_instanceRepository.GetByUser(user, TourInstances));
        }

        public void LinkTourInstancesWithTours()
        {
            foreach (TourInstance tourInstance in TourInstances)
            {
                Tour baseTour = _tourRepository.GetById(tourInstance.TourId);
                if (baseTour != null)
                {
                    tourInstance.BaseTour = baseTour;
                }
            }
        }



        private void Delete(object sender, RoutedEventArgs e)
        {
            if (SelectedTourInstance != null)
            {
                if ((SelectedTourInstance.Date - DateTime.Now).TotalHours >= 48)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure?", "Cancel tour",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {

                        _instanceRepository.Delete(SelectedTourInstance);
                        TourInstances.Remove(SelectedTourInstance);

                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("You can't cancel this tour", "Cancel tour",
                        MessageBoxButton.OK, MessageBoxImage.Question);
                }

            }
        }


    }
}
