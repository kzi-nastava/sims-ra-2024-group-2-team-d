using BookingApp.WPF.ViewModels.Owner;
using System.Windows;

namespace BookingApp.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for ForumCommentsOverview.xaml
    /// </summary>
    public partial class ForumsWindow : Window
    {
        ForumsViewModel viewModel;
        public ForumsWindow()
        {
            InitializeComponent();
            viewModel = new ForumsViewModel();
            DataContext = viewModel;
        }
    }
}
