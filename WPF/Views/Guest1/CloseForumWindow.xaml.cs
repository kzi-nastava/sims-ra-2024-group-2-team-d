using BookingApp.WPF.ViewModels.Guest;
using System.Windows;

namespace BookingApp.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for CloseForumWindow.xaml
    /// </summary>
    public partial class CloseForumWindow : Window
    {
        private CloseForumWindowViewModel viewModel;
        public CloseForumWindow()
        {
            InitializeComponent();
            viewModel = new CloseForumWindowViewModel();
            DataContext = viewModel;
        }
    }
}
