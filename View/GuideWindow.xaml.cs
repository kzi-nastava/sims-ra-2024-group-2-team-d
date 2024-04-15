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
        public static ObservableCollection<TourInstance> TourInstances { get; set; }

        public static ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        private readonly TourRepository _tourRepository;
        //private readonly TourInstanceRepository _tourInstanceRepository;
        private readonly KeyPointRepository _keyPointRepository;

        public Comment SelectedComment { get; set; }
        public TourInstance SelectedTourInstance { get; set; }

        public User LoggedInUser { get; set; }

        private readonly CommentRepository _repository;
        private readonly TourInstanceRepository _tourInstanceRepository;

        public GuideWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourInstanceRepository = new TourInstanceRepository();
            _tourRepository = new TourRepository();
            TourInstances = new ObservableCollection<TourInstance>(_tourInstanceRepository.GetAll());
            LinkTourInstancesWithTours();
            TourInstances = new ObservableCollection<TourInstance>(_tourInstanceRepository.GetForTheDay1(user, TourInstances));
        }

        public void LinkTourInstancesWithTours()
        {
            foreach (TourInstance tourInstance in TourInstances)
            {
                Tour baseTour = _tourRepository.GetById(tourInstance.TourId);
                if (baseTour != null)
                {
                    tourInstance.BaseTour = baseTour;
                }
            }
        }

        private void ShowCreateCommentForm(object sender, RoutedEventArgs e)
        {
            CreateTour createTour = new CreateTour(LoggedInUser);
            createTour.Show();
        }

        /*private void ShowViewCommentForm(object sender, RoutedEventArgs e)
        {
            if (SelectedComment != null)
            {
                CommentForm viewCommentForm = new CommentForm(SelectedComment);
                viewCommentForm.Show();
            }
        }*/

        private void StartTourButton_Click(object sender, RoutedEventArgs e)
        {
            /*if (SelectedTourInstance != null)
            {
                FollowingTourLive followingTourLive = new FollowingTourLive(SelectedTourInstance);
                followingTourLive.Show();
            }


            if (TourGrid.SelectedIndex == -1)
            {
                return;
            }*/

            TourInstance ti = TourGrid.SelectedItem as TourInstance;
            //otvara se prozor sa tom turom
            if (ti.End != true)
            {
                FollowTour ft = new FollowTour(ti);
                ft.Show();
                Close();
            }

        }

        private void ShowAllTours(object sender, RoutedEventArgs e)
        {
            AllTours allTours = new AllTours(LoggedInUser);
            allTours.Show();
        }

        private void statisticsButton_Click(object sender, RoutedEventArgs e)
        {
            Statistics statistics = new Statistics(LoggedInUser);
            statistics.Show();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Log out",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {                
                SignInForm signInForm = new SignInForm();
                signInForm.Show();
                this.Close();
            }
        }

        /* private void ShowUpdateCommentForm(object sender, RoutedEventArgs e)
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
         }*/


    }
}
