using BookingApp.Model;
using BookingApp.ViewModel;
using System.Windows;

namespace BookingApp.View.Guest1
{
    /// <summary>
    /// Interaction logic for RequestOverview.xaml
    /// </summary>
    public partial class RequestOverview : Window
    {
        private readonly User _user;
        private RequestsOverviewViewModel viewModel;
        public RequestOverview(User user)
        {
            InitializeComponent();
            _user = user;
            viewModel = new RequestsOverviewViewModel(user.Id);
            this.DataContext = viewModel;
        }

        private void ChangeReservation_Click(object sender, RoutedEventArgs e)
        {
            ReservationChange reservationChange = new ReservationChange(_user.Id, viewModel.Requests);
            reservationChange.ShowDialog();
        }

        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            //CancelReservation cancelReservation = new CancelReservation(_userId);
            //cancelReservation.ShowDialog();
        }
    }
}
