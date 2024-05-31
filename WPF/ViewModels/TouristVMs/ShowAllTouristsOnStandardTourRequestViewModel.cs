using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Services.IServices;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class ShowAllTouristsOnStandardTourRequestViewModel
    {
        private ITouristService _touristService;
        public ObservableCollection<Tourist> Tourists { get; set; }

        public int NumberOfTourists { get; set; }
        public ShowAllTouristsOnStandardTourRequestViewModel(TourRequestDTO tourRequest)
        {
            _touristService = Injector.Injector.CreateInstance<ITouristService>();
            Tourists = new ObservableCollection<Tourist>(_touristService.GetByIds(tourRequest.TouristsId));
            NumberOfTourists = tourRequest.NumberOfTourists;
        }
    }
}
