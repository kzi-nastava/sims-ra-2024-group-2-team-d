using BookingApp.Domain.Model;
using BookingApp.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class AllRequestsInComplexTourRequestViewModel
    {
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; }
        public User LoggedInUser {  get; set; }

        public ICommand GoBackCommand {  get; set; }

        public MainViewModel MainViewModel { get; set; }
        public AllRequestsInComplexTourRequestViewModel(MainViewModel mainViewModel, List<TourRequest> tourRequests, User loggedInUser) { 
            TourRequests = new ObservableCollection<TourRequestDTO>(ConvertModelToDTO(tourRequests));
            LoggedInUser = loggedInUser;
            MainViewModel = mainViewModel;
            GoBackCommand = new RelayCommand(GoBack);
        }

        public void GoBack()
        {
            MainViewModel.SwitchView(new MyComplexTourRequestsViewModel(MainViewModel, LoggedInUser));
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
    }
}
