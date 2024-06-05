using BookingApp.Domain.Model;
using BookingApp.Services.IServices;
using BookingApp.Validation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.ViewModels.Owner
{
    public class AccommodationPropositionViewModel
    : BindableBase, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private IStatisticService _statisticsService;
        private IAccommodationPropositionService _propositionService;
        private IAccommodationService _accommodationService;
        private IUserService _userService;

        public ObservableCollection<Location> _bestLocations;
        public ObservableCollection<Location> _worstLocations;

        public ObservableCollection<Location> BestLocations
        {
            get { return _bestLocations; }
            set
            {
                _bestLocations = value;
                OnPropertyChanged(nameof(BestLocations));
            }
        }

        public ObservableCollection<Location> WorstLocations
        {
            get { return _worstLocations; }
            set
            {
                _worstLocations = value;
                OnPropertyChanged(nameof(WorstLocations));
            }
        }

        public AccommodationPropositionViewModel()
        {
            _statisticsService = Injector.Injector.CreateInstance<IStatisticService>();
            _propositionService = Injector.Injector.CreateInstance<IAccommodationPropositionService>();
            _userService = Injector.Injector.CreateInstance<IUserService>();
            _accommodationService = Injector.Injector.CreateInstance<IAccommodationService>();

            var mostReservations = _propositionService.AccommodationWithMostReservations(_userService.GetUserId());
            var bestStatistic = _propositionService.HotAccommodationStatistics(_userService.GetUserId());

            var best = new List<Location>();
            if(mostReservations > 0)
            {
                best.Add(_accommodationService.GetById(mostReservations).Location);
            }

            if (bestStatistic > 0 && bestStatistic != mostReservations)
            {
                best.Add(_accommodationService.GetById(bestStatistic).Location);
            }
            

                BestLocations = new ObservableCollection<Location>(best.DistinctBy(l=>l.Id));



            var lowestReservations = _propositionService.AccommodationWithLeastReservations(_userService.GetUserId());
            var worstStatistic = _propositionService.ColdAccommodationStatistics(_userService.GetUserId());

            var worst = new List<Location>();

            if (lowestReservations > 0 && bestStatistic!= lowestReservations && mostReservations != lowestReservations)
            {
                worst.Add(_accommodationService.GetById(lowestReservations).Location);
            }

            if (worstStatistic > 0 && worstStatistic != lowestReservations &&  bestStatistic != worstStatistic && mostReservations != worstStatistic)
            {
                worst.Add(_accommodationService.GetById(worstStatistic).Location);
            }


            WorstLocations = new ObservableCollection<Location>(worst.DistinctBy(l => l.Id));

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
