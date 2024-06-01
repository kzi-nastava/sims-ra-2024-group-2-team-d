using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class CreateComplexTourRequestViewModel: INotifyPropertyChanged
    {
        public MainViewModel MainViewModel { get; set; }
        public User LoggedInUser {  get; set; }

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

        public Tourist InputTourist { get; set; }

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

        public TouristService TouristService { get; set; }
        public ObservableCollection<TourRequestDTO> AddedTourRequests {  get; set; }
        private readonly IDialogService _dialogService;

        public ComplexTourRequest NewComplexTourRequest { get; set; }
        public ComplexTourRequestService ComplexTourRequestService {  get; set; }
        public TourRequestService TourRequestService { get; set; }
        public CreateComplexTourRequestViewModel(MainViewModel _mainViewModel, User loggedInUser, IDialogService dialogService) { 
            MainViewModel = _mainViewModel;
            LoggedInUser = loggedInUser;
            Tourists = new ObservableCollection<Tourist>();
            NewTourRequest = new TourRequestDTO(LoggedInUser.Id);
            InputTourist = new Tourist();
            TouristService = new TouristService(Injector.Injector.CreateInstance<ITouristRepository>());
            TourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            ComplexTourRequestService = new ComplexTourRequestService(Injector.Injector.CreateInstance<IComplexTourRequestRepository>(), Injector.Injector.CreateInstance<ITourRequestRepository>());
            AddTouristCommand = new RelayCommand(AddTourist);
            AddRequestCommand = new RelayCommand(AddRequest);
            SendRequestCommand = new RelayCommand(SendRequest);
            AddUser();
            NewComplexTourRequest = new ComplexTourRequest(LoggedInUser.Id);
            AddedTourRequests = new ObservableCollection<TourRequestDTO>();
            _dialogService = dialogService;
            BackButtonCommand = new RelayCommand(GoBack);
        }

        public void GoBack()
        {
            MainViewModel.SwitchView(new TouristHomeViewModel(MainViewModel, LoggedInUser, new DialogService()));
        }
        public void SendRequest()
        {
            var confirmationViewModel = new ConfirmationDialogViewModel("Are you sure you want to create a complex tour request?");
            bool? result = _dialogService.ShowDialog(confirmationViewModel);
            if(result == true)
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
                if(feedbackResult==true)
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
