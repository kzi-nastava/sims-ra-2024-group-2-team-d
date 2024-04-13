using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
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
        public TouristReview TouristReview { get; set; }
        public TourReviewRepository TourReviewRepository { get; set; }
        public SpecificGuideReview(TouristReview tr)
        {
            InitializeComponent();
            TourReviewRepository = new TourReviewRepository();
            TouristReview = tr;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("This review is now invalid", "Review",
            MessageBoxButton.OK, MessageBoxImage.Question);

            TouristReview.TourReview.IsValid = false;
            TourReviewRepository.Update(TouristReview.TourReview);
            this.Close();

        }
    }
}
