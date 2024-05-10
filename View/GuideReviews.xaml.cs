using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.ViewModel.Guide;
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
    /// Interaction logic for GuideReviews.xaml
    /// </summary>
    public partial class GuideReviews : Window
    {
        //public TourInstance TourInstance { get; set; }

        //public User LoggedInUser { get; set; }

        //public List<TourReview> tourReviews { get; set; }

        //public TouristReview TouristReview { get; set; }
        //public List<Tourist> Tourists { get; set; }

        //public ObservableCollection<TouristReview> touristReviews { get; set; }
        //public TourReviewRepository TourReviewRepository { get; set; }
        //public TouristRepository TouristRepository { get; set; }
        //public FollowingTourLiveRepository FollowingTourLiveRepository { get; set; }
        //public KeyPointRepository _keyPointRepository { get; set; }

        public GuideReviews(TourInstance tourInstance)
        {
            InitializeComponent();
            DataContext = new GuideReviewsViewModel(tourInstance);
            //TourReviewRepository = new TourReviewRepository();
            //TouristRepository= new TouristRepository();
            //FollowingTourLiveRepository= new FollowingTourLiveRepository();
            //_keyPointRepository= new KeyPointRepository();
            //TourInstance = tourInstance;

            //TouristReview touristReview = new TouristReview();
            //Tourists = new List<Tourist>();
            //touristReviews = new ObservableCollection<TouristReview>();
            //GenerateReviews();
        }

        //public void GenerateReviews()
        //{
        //    List<TourReview> list = TourReviewRepository.GetAllByTourId(TourInstance.Id);
        //    List<TouristReview> reviews=new List<TouristReview>();
        //    foreach (var item in list)
        //    {
        //        Tourist t= TouristRepository.GetById(item.UserId);

        //        TouristReview tr = new TouristReview();
        //        tr.TourReview = item;
        //        tr.UserId = t.UserId;
        //        tr.FullName = t.Name + " " + t.LastName;
        //        tr.KeyPointId= FollowingTourLiveRepository.GetKeyPoint(TourInstance.Id,t.Id);
        //        tr.KeyPointName = _keyPointRepository.GetKeyPointName(FollowingTourLiveRepository.GetKeyPoint(TourInstance.Id, t.Id));

        //        touristReviews.Add(tr);
        //    }
        //    ReviewGrid.ItemsSource = touristReviews;
        //    ReviewGrid.DataContext = touristReviews;
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if(ReviewGrid.SelectedIndex==-1)
        //    {
        //        MessageBox.Show("Must select specific tour");
        //        return;
        //    }
        //    SpecificGuideReview specificGuideReview = new SpecificGuideReview(ReviewGrid.SelectedItem as TouristReview);
        //    specificGuideReview.ShowDialog();
        //    touristReviews.Clear();
        //    GenerateReviews();

        //}

        
    }
}
