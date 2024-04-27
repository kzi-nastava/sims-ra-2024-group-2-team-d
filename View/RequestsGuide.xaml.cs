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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for RequestsGuide.xaml
    /// </summary>
    public partial class RequestsGuide : Window
    {
        public User LoggedInUser { get; set; }
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public TourRequestRepository _tourRequestRepository { get; set; }
        public TourInstance TourInstance { get; set; }
        public TourInstanceRepository _tourInstanceRepository { get; set; }

        public RequestsGuide(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            _tourInstanceRepository = new TourInstanceRepository();
            
            _tourRequestRepository = new TourRequestRepository();
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestRepository.GetAll());
            GenerateTourRequests();
           
        }

        public void GenerateTourRequests()
        {
            TourRequests = new ObservableCollection<TourRequest>(TourRequests.Where(x => x.CurrentStatus == Status.OnHold).ToList());
            RequestsGrid.ItemsSource = TourRequests;
            RequestsGrid.DataContext = TourRequests;
        }



        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Must select specific tour request");
                return;
            }

            TourRequest selectedRequest = (TourRequest)RequestsGrid.SelectedItem;

            AcceptReqGuide acceptReqGuide = new AcceptReqGuide(selectedRequest, LoggedInUser);
            acceptReqGuide.ShowDialog();
            TourRequests.Clear();
            GenerateTourRequests();

            /*MessageBox.Show("You have accepted selected tour request!");

             TourRequest selectedRequest = (TourRequest)RequestsGrid.SelectedItem;
             selectedRequest.CurrentStatus = Status.Accepted;
             _tourRequestRepository.Update(selectedRequest);
             CreateTourInstanceFromRequest(selectedRequest);



             TourRequests.Remove(selectedRequest);

             RequestsGrid.ItemsSource = TourRequests;
             RequestsGrid.DataContext = TourRequests;*/

        }

        /*public void CreateTourInstanceFromRequest(TourRequest request)
        {
            TourInstance = new TourInstance(request.Id);
            _tourInstanceRepository.Save(TourInstance);
        }*/
    }
}
