using BookingApp.WPF.ViewModels.Guest;
using System.Windows;

namespace BookingApp.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for ForumsOverviewWindow.xaml
    /// </summary>
    public partial class ForumsOverviewWindow : Window
    {
        private ForumsOverviewViewModel viewModel;
        public ForumsOverviewWindow()
        {
            InitializeComponent();
            viewModel = new ForumsOverviewViewModel();
            DataContext = viewModel;
        }
    }
}
