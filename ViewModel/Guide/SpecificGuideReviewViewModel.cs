using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
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
    public class SpecificGuideReviewViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public TouristReview TouristReview { get; set; }
        public ObservableCollection<TourReview> TourReviews { get; set; }
        public MyCommand InvalidCommand { get; set; }

        public SpecificGuideReviewViewModel(TouristReview touristReview)
        {
            MainService = MainService.GetInstance();
            TouristReview = touristReview;
            TourReviews = new ObservableCollection<TourReview>();
            TourReviews.Add(TouristReview.TourReview);
            InvalidCommand = new MyCommand(Invalid);
        }

        private void Invalid()
        {
            MessageBoxResult result = MessageBox.Show("This review is now invalid", "Review",
            MessageBoxButton.OK, MessageBoxImage.Question);

            TouristReview.TourReview.IsValid = false;
            MainService.TourReviewService.Update(TouristReview.TourReview);
            //this.Close();

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
