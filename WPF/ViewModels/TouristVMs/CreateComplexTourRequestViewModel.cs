using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class CreateComplexTourRequestViewModel : INotifyPropertyChanged
    {
        public MainViewModel MainViewModel { get; set; }
        public User LoggedInUser { get; set; }

        private ObservableCollection<Tourist> tourists;
        public ObservableCollection<Tourist> Tourists
        {
            get => tourists;
            set
            {
                if (value != tourists)
                {
                    tourists = value;
                    OnPropertyChanged("Tourists");
                }
            }
        }

        private TouristDTO inputTourist { get; set; }
        public TouristDTO InputTourist
        {
            get => inputTourist;
            set
            {
                if (value != inputTourist)
                {
                    inputTourist = value;
                    OnPropertyChanged("InputTourist");
                }
            }
        }

        private TourRequestDTO newTourRequest;
        public TourRequestDTO NewTourRequest
        {
            get => newTourRequest;
            set
            {
                if (value != newTourRequest)
                {
                    newTourRequest = value;
                    OnPropertyChanged("NewTourRequest");
                }
            }
        }
        public ICommand AddTouristCommand { get; set; }

        public ICommand SendRequestCommand { get; set; }

        public ICommand AddRequestCommand { get; set; }
        public ICommand BackButtonCommand { get; set; }

        public ITouristService TouristService { get; set; }
        public ObservableCollection<TourRequestDTO> AddedTourRequests { get; set; }
        private readonly IDialogService _dialogService;
        public ICommand ShowAllAddedRequestsCommand { get; set; }

        public ComplexTourRequest NewComplexTourRequest { get; set; }
        public IComplexTourRequestService ComplexTourRequestService { get; set; }
        public ITourRequestService TourRequestService { get; set; }
        public CreateComplexTourRequestViewModel(MainViewModel _mainViewModel, User loggedInUser, IDialogService dialogService)
        {
            MainViewModel = _mainViewModel;
            LoggedInUser = loggedInUser;
            Tourists = new ObservableCollection<Tourist>();
            NewTourRequest = new TourRequestDTO(LoggedInUser.Id);
            InputTourist = new TouristDTO();            
            TouristService = Injector.Injector.CreateInstance<ITouristService>();
            TourRequestService = Injector.Injector.CreateInstance<ITourRequestService>();
            ComplexTourRequestService = Injector.Injector.CreateInstance<IComplexTourRequestService>();
            AddTouristCommand = new RelayCommand(AddTourist);
            AddRequestCommand = new RelayCommand(AddRequest);
            SendRequestCommand = new RelayCommand(SendRequest);
            AddUser();
            NewComplexTourRequest = new ComplexTourRequest(LoggedInUser.Id);
            AddedTourRequests = new ObservableCollection<TourRequestDTO>();
            _dialogService = dialogService;
            BackButtonCommand = new RelayCommand(GoBack);
            ShowAllAddedRequestsCommand = new RelayCommand(ShowAllAddedRequests);
        }

        public void ShowAllAddedRequests()
        {
            var confirmationViewModel = new ShowAllAddedRequestsToComplexTourRequestViewModel(AddedTourRequests);
            bool? result = _dialogService.ShowDialog(confirmationViewModel);
        }

        public void GoBack()
        {
            MainViewModel.SwitchView(new TouristHomeViewModel(MainViewModel, LoggedInUser, new DialogService()));
        }
        public void SendRequest()
        {
            var confirmationViewModel = new ConfirmationDialogViewModel("Are you sure you want to create a complex tour request?");
            bool? result = _dialogService.ShowDialog(confirmationViewModel);
            if (result == true)
            {
                foreach (var tourRequest in NewComplexTourRequest.TourRequests)
                {
                    foreach (var tourist in tourRequest.Tourists)
                    {
                        Tourist savedTourist = TouristService.Save(tourist);
                        tourRequest.TouristsId.Add(savedTourist.Id);
                    }
                    TourRequest savedTourRequest = TourRequestService.Save(tourRequest);
                    NewComplexTourRequest.TourRequestIds.Add(savedTourRequest.Id);
                }
                ComplexTourRequestService.Save(NewComplexTourRequest);
                var feedbackViewModel = new FeedbackDialogViewModel("Complex tour request has been created!");
                bool? feedbackResult = _dialogService.ShowDialog(feedbackViewModel);
                if (feedbackResult==true)
                {
                    MainViewModel.SwitchView(new TouristHomeViewModel(MainViewModel, LoggedInUser, new DialogService()));
                }
            }
        }
        public void AddUser()
        {
            Tourist userTourist = new Tourist(LoggedInUser.FirstName, LoggedInUser.LastName, LoggedInUser.Age, LoggedInUser.Id);
            Tourists.Add(userTourist);
        }
        public void AddTourist()
        {
            Tourist tourist = new Tourist(InputTourist.Name, InputTourist.LastName, InputTourist.Age);
            Tourists.Add(tourist);
            NewTourRequest.NumberOfTouristsCounter--;
            NewTourRequest.NumberOfTourists++;
            InputTourist = new TouristDTO();
        }

        public void AddRequest()
        {
            NewTourRequest.Tourists = Tourists.ToList();
            NewTourRequest.UpdateStartEndString();
            TourRequest newTourRequest = NewTourRequest.ToTourRequest();
            newTourRequest.IsPartOfComplexRequest = true;
            NewComplexTourRequest.TourRequests.Add(newTourRequest);
            AddedTourRequests.Add(NewTourRequest);
            NewTourRequest = new TourRequestDTO(LoggedInUser.Id);
            Tourists = new ObservableCollection<Tourist>();
            AddUser();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
