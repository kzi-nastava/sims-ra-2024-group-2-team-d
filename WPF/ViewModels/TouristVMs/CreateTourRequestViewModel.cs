using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class CreateTourRequestViewModel : INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }
        public ObservableCollection<Tourist> Tourists { get; set; }
        private Tourist inputTourist;

        public Tourist InputTourist
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
        public TourRequestDTO NewTourRequest { get; set; }

        public ICommand AddTouristCommand { get; set; }

        public ICommand SendRequestCommand { get; set; }

        public ITouristService TouristService { get; set; }

        public ITourRequestService TourRequestService { get; set; }

        private readonly MainViewModel _mainViewModel;
        public ICommand BackButtonCommand { get; set; }
        public IDialogService DialogService { get; set; }
        public ICommand DeleteTouristCommand { get; set; }
        public ICommand EditTouristCommand { get; set; }
        public CreateTourRequestViewModel(MainViewModel mainViewModel, User loggedInUser, IDialogService dialogService)
        {
            LoggedInUser = loggedInUser;
            Tourists = new ObservableCollection<Tourist>();
            NewTourRequest = new TourRequestDTO(LoggedInUser.Id);
            InputTourist = new Tourist();
            TouristService = Injector.Injector.CreateInstance<ITouristService>();
            TourRequestService = Injector.Injector.CreateInstance<ITourRequestService>();
            AddTouristCommand = new RelayCommand(AddTourist);
            _mainViewModel = mainViewModel;
            SendRequestCommand = new RelayCommand(() =>
            {
                SendRequest();
            });
            AddUser();
            BackButtonCommand = new RelayCommand(GoBack);
            DialogService = dialogService;
            DeleteTouristCommand = new RelayCommand(tourist => DeleteTourist((Tourist)tourist));
            EditTouristCommand = new RelayCommand(tourist => EditTourist((Tourist)tourist));
        }

        public void EditTourist(Tourist tourist)
        {
            if (tourist.UserId == -1)
            {
                InputTourist = new Tourist(tourist.Name, tourist.LastName, tourist.Age, tourist.UserId);
                Tourists.Remove(tourist);
                NewTourRequest.NumberOfTourists--;
            }
            else
            {
                var feedbackViewModel = new FeedbackDialogViewModel("You can't edit yourself!");
                bool? feedbackResult = DialogService.ShowDialog(feedbackViewModel);
            }
        }

        public void DeleteTourist(Tourist tourist)
        {
            if (tourist.UserId == -1)
            {
                var confirmationViewModel = new ConfirmationDialogViewModel("Are you sure you want to delete this tourist?");
                bool? result = DialogService.ShowDialog(confirmationViewModel);
                if (result == true)
                {
                    Tourists.Remove(tourist);
                    NewTourRequest.NumberOfTourists--;
                }
            }
            else
            {
                var feedbackViewModel = new FeedbackDialogViewModel("You can't delete yourself!");
                bool? feedbackResult = DialogService.ShowDialog(feedbackViewModel);
            }


        }

        public void GoBack()
        {
            var confirmationViewModel = new ConfirmationDialogViewModel("Are you sure you want to exit?");
            bool? result = DialogService.ShowDialog(confirmationViewModel);
            if(result == true)
            {
                _mainViewModel.SwitchView(new TouristHomeViewModel(_mainViewModel, LoggedInUser, new DialogService()));
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
            InputTourist = new Tourist();
        }

        public void SendRequest()
        {
            var confirmationViewModel = new ConfirmationDialogViewModel("Are you sure you want to create a tour request?");          
            bool? result = DialogService.ShowDialog(confirmationViewModel);
            if(result == true) {
                var feedbackViewModel = new FeedbackDialogViewModel("Tour request has been created!");
                bool? feedbackResult = DialogService.ShowDialog(feedbackViewModel);
                foreach (var tourist in Tourists)
                {
                    Tourist savedTourist = TouristService.Save(tourist);
                    NewTourRequest.TouristsId.Add(savedTourist.Id);
                }
                TourRequest tourRequest = NewTourRequest.ToTourRequest();
                TourRequestService.Save(tourRequest);
                _mainViewModel.SwitchView(new TouristHomeViewModel(_mainViewModel, LoggedInUser, new DialogService()));
            }           
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
