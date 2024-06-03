using BookingApp.WPF.ViewModels.Owner;
using System.Windows;

namespace BookingApp.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for ForumCommentsOverview.xaml
    /// </summary>
    public partial class ForumCommentsWindow : Window
    {
        ForumCommentsViewModel viewModel;
        public ForumCommentsWindow()
        {
            InitializeComponent();
            viewModel = new ForumCommentsViewModel();
            DataContext = viewModel;
        }

        private void Button_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
