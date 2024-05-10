using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookingApp.Services;
using BookingApp.Dto;
using BookingApp.Model;

namespace BookingApp.WPF.ViewModels
{
    public class CreateTourRequestViewModel : INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }
        public ObservableCollection<Tourist> Tourists {  get; set; }

        public Tourist InputTourist { get; set; }
        public TourRequestDTO NewTourRequest { get; set; }

        public ICommand AddTouristCommand { get; set; }

        public ICommand SendRequestCommand { get; set; }

        public TouristService TouristService { get; set; }

        public TourRequestService TourRequestService { get; set; }

        public string Start {  get; set; }
        public CreateTourRequestViewModel(User loggedInUser, Action closeAction) {
            LoggedInUser = loggedInUser;
            Tourists = new ObservableCollection<Tourist>();
            NewTourRequest = new TourRequestDTO(LoggedInUser.Id);
            InputTourist = new Tourist();
            TouristService = new TouristService();
            TourRequestService = new TourRequestService();
            AddTouristCommand = new RelayCommand(AddTourist);
            SendRequestCommand = new RelayCommand(() =>
            {
                SendRequest();
                closeAction();
            });
            AddUser();
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
        }

        public void SendRequest()
        {
            foreach(var tourist in Tourists)
            {
                Tourist savedTourist = TouristService.Save(tourist);
                NewTourRequest.TouristsId.Add(savedTourist.Id);
            }
            TourRequest tourRequest = NewTourRequest.ToTourRequest();
            TourRequestService.Save(tourRequest);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
