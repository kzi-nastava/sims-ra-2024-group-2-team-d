using BookingApp.Dto;
using BookingApp.Services.IServices;
using BookingApp.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.Owner
{
    internal class MonthlyStatisticsViewModel : BindableBase, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private IStatisticService _accommodationStatisticsService;
        private int AccommodationId;
        private int Year;

        public ObservableCollection<MonthStatisticDto> _monthlyStatistics;

        public ObservableCollection<MonthStatisticDto> MonthStatisticDtos
        {
            get { return _monthlyStatistics; }
            set
            {
                _monthlyStatistics = value;
                OnPropertyChanged(nameof(MonthStatisticDtos));
            }
        }

        public MonthlyStatisticsViewModel(int accommodationId, int year)
        {
            _accommodationStatisticsService = Injector.Injector.CreateInstance<IStatisticService>();
            AccommodationId = accommodationId;
            Year = year;

            MonthStatisticDtos = new ObservableCollection<MonthStatisticDto>(_accommodationStatisticsService.MonthStatisticsForYear(AccommodationId, Year));

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
