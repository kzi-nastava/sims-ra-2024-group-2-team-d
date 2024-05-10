using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModel.Guide
{
    public class GuideReviewsViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public TourInstance TourInstance;
        public ObservableCollection<TouristReview> TouristReviews { get; set; }
        private TouristReview TouristReview;
        public TouristReview SelectedTouristReview
        {
            get { return TouristReview; }
            set
            {
                TouristReview = value;
                OnPropertyChanged("SelectedTouristReview");
            }
        }
        public MyCommand OpenCommand { get; set; }

        public GuideReviewsViewModel(TourInstance tourInstance)
        {
            MainService = MainService.GetInstance();
            TourInstance = tourInstance;
            TouristReviews = new ObservableCollection<TouristReview>();
            GenerateReviews();
            OpenCommand = new MyCommand(Open);
        }

        private void Open()
        {
            if (SelectedTouristReview == null)
            {
                MessageBox.Show("Must select specific tour");
                return;
            }
            SpecificGuideReview specificGuideReview = new SpecificGuideReview(SelectedTouristReview);
            specificGuideReview.ShowDialog();
            TouristReviews.Clear();
            GenerateReviews();
        }

        public void GenerateReviews()
        {
            List<TourReview> list = MainService.TourReviewService.GetAllByTourId(TourInstance.Id);
            //List<TouristReview> reviews = new List<TouristReview>();
            foreach (var item in list)
            {
                Tourist t = MainService.TouristService.GetById(item.UserId);
                TouristReview tr = new TouristReview();
                tr.TourReview = item;
                tr.UserId = t.UserId;
                tr.FullName = t.Name + " " + t.LastName;
                tr.KeyPointId = MainService.FollowingTourLiveService.GetKeyPoint(TourInstance.Id, t.Id);
                tr.KeyPointName = MainService.KeyPointService.GetKeyPointName(MainService.FollowingTourLiveService.GetKeyPoint(TourInstance.Id, t.Id));

                TouristReviews.Add(tr);
            }
            //ReviewGrid.ItemsSource = touristReviews;
            //ReviewGrid.DataContext = touristReviews;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
