using BookingApp.WPF.ViewModels.Guest;
using System.Windows;

namespace BookingApp.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for CreatingForumForm.xaml
    /// </summary>
    public partial class CreatingForumForm : Window
    {
        private CreatingForumViewModel viewModel;
        public CreatingForumForm()
        {
            InitializeComponent();
            viewModel = new CreatingForumViewModel();
            DataContext = viewModel;
        }
    }
}
