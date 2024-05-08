using BookingApp.Dto;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;

namespace BookingApp.WPF.ViewModels
{
    public class ShowAllTouristsOnStandardTourRequestViewModel
    {
        private TouristService _touristService;
        public ObservableCollection<Tourist> Tourists {  get; set; }

        public int NumberOfTourists {  get; set; }
        public ShowAllTouristsOnStandardTourRequestViewModel(TourRequestDTO tourRequest) {
            _touristService = new TouristService();
            Tourists = new ObservableCollection<Tourist>(_touristService.GetByIds(tourRequest.TouristsId));
            NumberOfTourists = tourRequest.NumberOfTourists;
        }
    }
}
