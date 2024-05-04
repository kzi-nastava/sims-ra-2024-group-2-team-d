using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for RequestsGuide.xaml
    /// </summary>
    public partial class RequestsGuide : Window,INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        private TourRequest tourReq;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public TourRequest SelectedTour
        {
            get
            {
                return this.tourReq;
            }

            set
            {
                this.tourReq = value;
                OnPropertyChanged("SelectedTour");

            }
        }
        public TourRequestRepository _tourRequestRepository { get; set; }
        public TourInstance TourInstance { get; set; }
        public TourInstanceRepository _tourInstanceRepository { get; set; }

        public RequestsGuide(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            _tourInstanceRepository = new TourInstanceRepository();
            
            _tourRequestRepository = new TourRequestRepository();
            //TourRequests = new ObservableCollection<TourRequest>(_tourRequestRepository.GetAll());
            GenerateTourRequests();
           
        }

        public void GenerateTourRequests()
        {
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestRepository.getByStatus());
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
            
            GenerateTourRequests();

        }

        private void SearchByLocation_Click(object sender, RoutedEventArgs e)
        {
            if (SearchLocation.Text == "") return;
            string Location = SearchLocation.Text;
         
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestRepository.FilterByLocation(Location));
            RequestsGrid.ItemsSource = TourRequests;
            RequestsGrid.DataContext = TourRequests;
        }

        private void SearchByNum_Click(object sender, RoutedEventArgs e)
        {
            if (SearchNum.Text == "") return;
            int Num = Convert.ToInt32(SearchNum.Text);

            TourRequests = new ObservableCollection<TourRequest>(_tourRequestRepository.FilterByNumOfTourists(Num));
            RequestsGrid.ItemsSource = TourRequests;
            RequestsGrid.DataContext = TourRequests;
        }

        private void SearchByLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (SearchLanguage.Text == "") return;
            string Language = SearchLanguage.Text;

            TourRequests = new ObservableCollection<TourRequest>(_tourRequestRepository.FilterByLanguage(Language));
            RequestsGrid.ItemsSource = TourRequests;
            RequestsGrid.DataContext = TourRequests;
        }

        private void SearchByDate_Click(object sender, RoutedEventArgs e)
        {
            if (StartDate.Text == "") return;
            if (EndDate.Text == "") return;

            DateOnly Start = DateOnly.ParseExact(StartDate.Text, "yyyy-MM-dd", null);
            DateOnly End = DateOnly.ParseExact(EndDate.Text, "yyyy-MM-dd", null);
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestRepository.FilterByDateRange(Start, End));
            RequestsGrid.ItemsSource = TourRequests;
            RequestsGrid.DataContext = TourRequests;

        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {

        }

        /*public void CreateTourInstanceFromRequest(TourRequest request)
        {
            TourInstance = new TourInstance(request.Id);
            _tourInstanceRepository.Save(TourInstance);
        }*/
    }
}
