using BookingApp.Dto;
using BookingApp.Services.IServices;
using BookingApp.Validation;
using BookingApp.WPF.Views.Owner;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.WPF.ViewModels.Owner
{
    public class StatisticsViewModel : BindableBase, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;


        private readonly IStatisticService _accommodationStatisticsService;


        public ObservableCollection<YearStatisticDto> _statisticsByYear;
        public ObservableCollection<YearStatisticDto> YearStatisticDtos
        {
            get { return _statisticsByYear; }
            set
            {
                _statisticsByYear = value;
                OnPropertyChanged(nameof(YearStatisticDtos));
            }
        }

        private int AccommodationId;

        public YearStatisticDto SelectedYear { get; set; }

        public RelayCommand MonthStatistics { get; set; }


        public StatisticsViewModel(int accommodationId)
        {
            _accommodationStatisticsService = Injector.Injector.CreateInstance<IStatisticService>();
            AccommodationId = accommodationId;

            YearStatisticDtos = new ObservableCollection<YearStatisticDto>(_accommodationStatisticsService.YearStatisticsForAccommodation(AccommodationId));

            MonthStatistics = new RelayCommand(Month_ButtonClick);
        }

        private void Month_ButtonClick(object parameter)
        {
            if (SelectedYear == null)
            {
                MessageBox.Show("Please selecte a year");
            }
            else
            {
                MonthlyStatistics monthlyStatistics = new MonthlyStatistics(AccommodationId, SelectedYear.Year);
                monthlyStatistics.Show();
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
