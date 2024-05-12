using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels.Guide;
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
    /// Interaction logic for SpecificGuideReview.xaml
    /// </summary>
    public partial class SpecificGuideReview : Window
    {
        //public TouristReview TouristReview { get; set; }
        //public TourReviewRepository _tourReviewRepository { get; set; }
        //public ObservableCollection<TourReview> tourReviews { get; set; }

        public SpecificGuideReview(TouristReview touristReview)
        {
            InitializeComponent();
            DataContext = new SpecificGuideReviewViewModel(touristReview);
            //_tourReviewRepository = new TourReviewRepository();
            //TouristReview = touristReview;
            //tourReviews = new ObservableCollection<TourReview>();
            //tourReviews.Add(TouristReview.TourReview);
            //GradesRevGrid.ItemsSource = tourReviews;
            //GradesRevGrid.DataContext = tourReviews;
            //ComRevGrid.ItemsSource = tourReviews;
            //ComRevGrid.DataContext = tourReviews;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult result = MessageBox.Show("This review is now invalid", "Review",
        //    MessageBoxButton.OK, MessageBoxImage.Question);

        //    TouristReview.TourReview.IsValid = false;
        //    _tourReviewRepository.Update(TouristReview.TourReview);
        //    this.Close();

        //}
    }
}
