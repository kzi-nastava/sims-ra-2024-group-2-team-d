using BookingApp.WPF.ViewModels.Owner;
using System.Windows;

namespace BookingApp.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationProposition.xaml
    /// </summary>
    public partial class AccommodationProposition : Window
    {
        private AccommodationPropositionViewModel viewModel;
        public AccommodationProposition()
        {
            InitializeComponent();
            viewModel = new AccommodationPropositionViewModel();
            DataContext = viewModel;
        }

    }
}
