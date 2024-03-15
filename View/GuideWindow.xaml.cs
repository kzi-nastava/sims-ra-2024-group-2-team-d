using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GuideWindow : Window
    {
        public static ObservableCollection<Comment> Comments { get; set; }

        public Comment SelectedComment { get; set; }

        public User LoggedInUser { get; set; }

        private readonly CommentRepository _repository;

        public GuideWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new CommentRepository();
            Comments = new ObservableCollection<Comment>(_repository.GetByUser(user));
        }

        private void ShowCreateCommentForm(object sender, RoutedEventArgs e)
        {
            CreateTour createTour = new CreateTour(LoggedInUser);
            createTour.Show();
        }

        private void ShowViewCommentForm(object sender, RoutedEventArgs e)
        {
            if (SelectedComment != null)
            {
                CommentForm viewCommentForm = new CommentForm(SelectedComment);
                viewCommentForm.Show();
            }
        }

        private void ShowUpdateCommentForm(object sender, RoutedEventArgs e)
        {
            if (SelectedComment != null)
            {
                CommentForm updateCommentForm = new CommentForm(SelectedComment, LoggedInUser);
                updateCommentForm.Show();
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (SelectedComment != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete comment",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _repository.Delete(SelectedComment);
                    Comments.Remove(SelectedComment);
                }
            }
        }


    }
}
