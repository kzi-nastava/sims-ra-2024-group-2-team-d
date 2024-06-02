using BookingApp.WPF.ViewModels.Guest;
using System.Windows;

namespace BookingApp.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for ForumCommentsOverview.xaml
    /// </summary>
    public partial class ForumCommentsOverview : Window
    {
        ForumCommentsOverviewViewModel viewModel;
        public ForumCommentsOverview()
        {
            InitializeComponent();
            viewModel = new ForumCommentsOverviewViewModel();
            DataContext = viewModel;
        }
    }
}
