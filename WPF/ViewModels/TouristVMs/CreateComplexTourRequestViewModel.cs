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
        public ICommand DeleteTouristCommand { get; set; }
        public ICommand EditTouristCommand { get; set; }
        private bool isPlusButtonEnabled;
        public bool IsPlusButtonEnabled
        {
            get => isPlusButtonEnabled;
            set
            {
                if(isPlusButtonEnabled != value)
                {
                    isPlusButtonEnabled = value;
                    OnPropertyChanged(nameof(IsPlusButtonEnabled));
                }
            }
        }
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
            DeleteTouristCommand = new RelayCommand(tourist => DeleteTourist((Tourist)tourist));
            EditTouristCommand = new RelayCommand(tourist => EditTourist((Tourist)tourist));
            IsPlusButtonEnabled = false;
        }

        public void EditTourist(Tourist tourist)
        {
            if (tourist.UserId == -1)
            {
                InputTourist = new TouristDTO(tourist);
                Tourists.Remove(tourist);
                NewTourRequest.NumberOfTourists--;
            }
            else
            {
                var feedbackViewModel = new FeedbackDialogViewModel("You can't edit yourself!");
                bool? feedbackResult = _dialogService.ShowDialog(feedbackViewModel);
            }
        }

        public void DeleteTourist(Tourist tourist)
        {
            if (tourist.UserId == -1)
            {
                var confirmationViewModel = new ConfirmationDialogViewModel("Are you sure you want to delete this tourist?");
                bool? result = _dialogService.ShowDialog(confirmationViewModel);
                if (result == true)
                {
                    Tourists.Remove(tourist);
                    NewTourRequest.NumberOfTourists--;
                }
            }
            else
            {
                var feedbackViewModel = new FeedbackDialogViewModel("You can't delete yourself!");
                bool? feedbackResult = _dialogService.ShowDialog(feedbackViewModel);
            }
        }

        public void ShowAllAddedRequests()
        {
            var confirmationViewModel = new ShowAllAddedRequestsToComplexTourRequestViewModel(AddedTourRequests, _dialogService);
            bool? result = _dialogService.ShowDialog(confirmationViewModel);
            if(result == true)
            {
               
                    AddedTourRequests = confirmationViewModel.AddedTourRequests;
                    TourRequest tourRequestForRemoval = NewComplexTourRequest.TourRequests.Find(t => t.Description == confirmationViewModel.SelectedRequest.Description && t.Language == confirmationViewModel.SelectedRequest.Language && t.Location == confirmationViewModel.SelectedRequest.Location);
                    NewComplexTourRequest.TourRequests.Remove(tourRequestForRemoval);
                if (confirmationViewModel.DeleteOrEdit == "Edit")
                {
                    NewTourRequest = confirmationViewModel.SelectedRequest;
                    Tourists =  new ObservableCollection<Tourist>(confirmationViewModel.SelectedRequest.Tourists);
                }
                
            }
        }

        public void GoBack()
        {
            var confirmationViewModel = new ConfirmationDialogViewModel("Are you sure you want to exit?");
            bool? result = _dialogService.ShowDialog(confirmationViewModel);
            if(result == true)
            {
                MainViewModel.SwitchView(new TouristHomeViewModel(MainViewModel, LoggedInUser, new DialogService()));
            }                     
        }
        public void SendRequest()
        {
            if(NewComplexTourRequest.TourRequests.Count == 0)
            {
                var vm = new FeedbackDialogViewModel("Complex tour request must have at least one request!");
                bool? resultFB = _dialogService.ShowDialog(vm);
                return;
            }
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
            if(string.IsNullOrWhiteSpace(InputTourist?.Name) ||
                              string.IsNullOrWhiteSpace(InputTourist?.LastName) ||
                              InputTourist?.Age <= 0)
            {
                var feedbackViewModel = new FeedbackDialogViewModel("All fields for tourist information are required!");
                bool? feedbackResult = _dialogService.ShowDialog(feedbackViewModel);
                return;
            }
            Tourist tourist = new Tourist(InputTourist.Name, InputTourist.LastName, InputTourist.Age);
            Tourists.Add(tourist);
            NewTourRequest.NumberOfTouristsCounter--;
            NewTourRequest.NumberOfTourists++;
            InputTourist = new TouristDTO();
        }

        public void AddRequest()
        {
            if (string.IsNullOrWhiteSpace(NewTourRequest?.Location) || string.IsNullOrWhiteSpace(NewTourRequest?.Description) || string.IsNullOrWhiteSpace(NewTourRequest?.Language))
            {
                var feedbackViewModel = new FeedbackDialogViewModel("All fields for tour request information are required!");
                bool? feedbackResult = _dialogService.ShowDialog(feedbackViewModel);
                return;
            }
            NewTourRequest.Tourists = Tourists.ToList();
            NewTourRequest.UpdateStartEndString();
            TourRequest newTourRequest = NewTourRequest.ToTourRequest();
            newTourRequest.IsPartOfComplexRequest = true;
            NewComplexTourRequest.TourRequests.Add(newTourRequest);
            AddedTourRequests.Add(NewTourRequest);
            NewTourRequest = new TourRequestDTO(LoggedInUser.Id);
            Tourists = new ObservableCollection<Tourist>();
            AddUser();
            var feedbackVM = new FeedbackDialogViewModel("Request added to the complex tour request!");
            bool? result = _dialogService.ShowDialog(feedbackVM);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
