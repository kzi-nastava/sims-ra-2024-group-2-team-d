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

        public Tourist InputTourist { get; set; }
        public TourRequestDTO NewTourRequest { get; set; }

        public ICommand AddTouristCommand { get; set; }

        public ICommand SendRequestCommand { get; set; }

        public ITouristService TouristService { get; set; }

        public ITourRequestService TourRequestService { get; set; }

        private readonly MainViewModel _mainViewModel;
        public ICommand BackButtonCommand { get; set; }
        public CreateTourRequestViewModel(MainViewModel mainViewModel, User loggedInUser)
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
        }

        public void GoBack()
        {
            _mainViewModel.SwitchView(new TouristHomeViewModel(_mainViewModel, LoggedInUser, new DialogService()));
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
        }

        public void SendRequest()
        {
            foreach (var tourist in Tourists)
            {
                Tourist savedTourist = TouristService.Save(tourist);
                NewTourRequest.TouristsId.Add(savedTourist.Id);
            }
            TourRequest tourRequest = NewTourRequest.ToTourRequest();
            TourRequestService.Save(tourRequest);
            _mainViewModel.SwitchView(new TouristHomeViewModel(_mainViewModel, LoggedInUser, new DialogService()));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
