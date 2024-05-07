using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.Services;
using BookingApp.Dto;

namespace BookingApp.WPF.ViewModels
{
    public class MyStandardTourRequestsViewModel
    {
        public User LoggedInUser {  get; set; }
        public ObservableCollection<TourRequest> TourRequests { get; set; }

        public ObservableCollection<TourRequestDTO> MyTourRequests {  get; set; }

        public TourRequestService _tourRequestService {  get; set; }
        public MyStandardTourRequestsViewModel(User loggedInUser) {
            LoggedInUser = loggedInUser;
            _tourRequestService = new TourRequestService();
            MyTourRequests = new ObservableCollection<TourRequestDTO>(ConvertModelToDTO(_tourRequestService.GetByUserTouristId(LoggedInUser.Id)));
         
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
