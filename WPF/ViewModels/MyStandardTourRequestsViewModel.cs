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
using BookingApp.Domain.Model;

namespace BookingApp.WPF.ViewModels
{
    public class MyStandardTourRequestsViewModel
    {
        public User LoggedInUser {  get; set; }
        public ObservableCollection<TourRequest> TourRequests { get; set; }

        public ObservableCollection<TourRequestDTO> MyTourRequests {  get; set; }

        public TourRequestService _tourRequestService {  get; set; }

        public ICommand InfoCommand {  get; set; }
        public MyStandardTourRequestsViewModel(User loggedInUser) {
            LoggedInUser = loggedInUser;
            _tourRequestService = new TourRequestService();
            _tourRequestService.InvalidateOutdatedTourRequests();
            MyTourRequests = new ObservableCollection<TourRequestDTO>(ConvertModelToDTO(_tourRequestService.GetByUserTouristId(LoggedInUser.Id)));
            InfoCommand = new RelayCommand(tourRequest => ShowMoreInfo((TourRequestDTO)tourRequest));
         
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
    }
}
