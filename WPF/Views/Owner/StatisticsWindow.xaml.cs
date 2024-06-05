using BookingApp.WPF.ViewModels.Owner;
using System.Windows;

namespace BookingApp.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        private StatisticsViewModel viewModel;
        public Statistics(int accommodationId)
        {
            InitializeComponent();
            viewModel = new StatisticsViewModel(accommodationId);
            DataContext = viewModel;
        }
    }
}
