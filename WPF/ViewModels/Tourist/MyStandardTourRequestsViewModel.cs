using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Services;
using BookingApp.Dto;
using System.Windows.Input;
using BookingApp.WPF.Views;
using System.ComponentModel;
using System.Collections;
using LiveCharts;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.WPF.ViewModels
{
    public class MyStandardTourRequestsViewModel: INotifyPropertyChanged
    {
        public User LoggedInUser {  get; set; }
        public ObservableCollection<TourRequest> TourRequests { get; set; }

        public ObservableCollection<TourRequestDTO> MyTourRequests {  get; set; }

        public TourRequestService _tourRequestService {  get; set; }

        private double averageNumberOfTourists;

        public double AverageNumberOfTourists
        {
            get => averageNumberOfTourists;
            set
            {
                if (value != averageNumberOfTourists)
                {
                    averageNumberOfTourists = value;
                    OnPropertyChanged("AverageNumberOfTourists");
                }
            }
        }
        private SeriesCollection _pieSeriesCollection;
        public SeriesCollection PieSeriesCollection
        {
            get { return _pieSeriesCollection; }
            set { _pieSeriesCollection = value; OnPropertyChanged(nameof(PieSeriesCollection)); }
        }

        public List<string> DistinctYears { get; set; }

        public string SelectedYear {  get; set; }
        public ICommand YearSelectionChangedCommand {  get; set; }

        public ICommand InfoCommand {  get; set; }

        public ICommand ShowLanguageRequestCountGraphCommand {  get; set; }

        public ICommand ShowLocationRequestCountGraphCommand { get; set; }
        public MyStandardTourRequestsViewModel(User loggedInUser) {
            LoggedInUser = loggedInUser;
            _tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            _tourRequestService.InvalidateOutdatedTourRequests();
            MyTourRequests = new ObservableCollection<TourRequestDTO>(ConvertModelToDTO(_tourRequestService.GetByUserTouristId(LoggedInUser.Id)));
            InfoCommand = new RelayCommand(tourRequest => ShowMoreInfo((TourRequestDTO)tourRequest));
            AverageNumberOfTourists = _tourRequestService.CalculateAverageNumberOfTourists(LoggedInUser.Id);
            DistinctYears = new List<string>();
            DistinctYears.Add("All years");
            DistinctYears.AddRange(_tourRequestService.GetDistinctYearsForTourRequests(LoggedInUser.Id));
            YearSelectionChangedCommand = new RelayCommand(YearSelectionChanged);
            ShowLanguageRequestCountGraphCommand = new RelayCommand(ShowLanguageRequestCountGraph);
            ShowLocationRequestCountGraphCommand = new RelayCommand(ShowLocationRequestCountGraph);
        }
        public void ShowLocationRequestCountGraph()
        {
            Dictionary<string, int> locationRequestCountPair = _tourRequestService.GetLocationRequestCountPair();
            PlotGraphView view = new PlotGraphView(locationRequestCountPair,"Location");
            view.Show();
        }
        public void ShowLanguageRequestCountGraph()
        {
            Dictionary<string, int> languageRequestCountPair = _tourRequestService.GetLanguageRequestCountPair();
            PlotGraphView view = new PlotGraphView(languageRequestCountPair, "Language");
            view.Show();
        }
        public void YearSelectionChanged()
        {
            AverageNumberOfTourists = _tourRequestService.CalculateAverageNumberOfTourists(LoggedInUser.Id, SelectedYear);
            PieSeriesCollection = _tourRequestService.UpdatePie(LoggedInUser.Id, SelectedYear);
        }
        public List<TourRequestDTO> ConvertModelToDTO(List<TourRequest> tourRequests)
        {
            List<TourRequestDTO> dtos = new List<TourRequestDTO>();
            foreach (var tourRequest in tourRequests)
            {
                dtos.Add(new TourRequestDTO(tourRequest));
            }
            return dtos;
        }

        public void ShowMoreInfo(TourRequestDTO tourRequest)
        {
            ShowAllTouristsOnStandardTourRequestView view = new ShowAllTouristsOnStandardTourRequestView(tourRequest);
            view.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
