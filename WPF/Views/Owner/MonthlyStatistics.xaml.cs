using BookingApp.WPF.ViewModels.Owner;
using System.Windows;

namespace BookingApp.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for MonthlyStatistics.xaml
    /// </summary>
    public partial class MonthlyStatistics : Window
    {
        private MonthlyStatisticsViewModel viewModel;
        public MonthlyStatistics(int accommodationId, int year)
        {
            InitializeComponent();
            viewModel = new MonthlyStatisticsViewModel(accommodationId, year);
            DataContext = viewModel;
        }
    }
}
