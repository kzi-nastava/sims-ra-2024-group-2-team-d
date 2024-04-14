using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels
{
    public class UserTourReviewViewModel
    {
        public TourReview UserTourReview { get; set; }

        private TourReviewRepository _tourReviewRepository;

        public ICommand ConfirmReviewCommand {  get; set; }

        public UserTourReviewViewModel(User loggedInUser, TourInstance tourInstance, Action closeAction)
        {
            UserTourReview = new TourReview();
            UserTourReview.TourInstanceId = tourInstance.Id;
            UserTourReview.GuideId = tourInstance.BaseTour.UserId;
            UserTourReview.UserId = loggedInUser.Id;
            ConfirmReviewCommand = new RelayCommand(() =>
            {
                ConfirmReview(tourInstance.BaseTour.Id);
                closeAction();
            });

        }

        public void ConfirmReview(int tourId)
        {
            
            TourReviewRepository _tourReviewRepository = new TourReviewRepository();
            _tourReviewRepository.Save(UserTourReview);

            
        }

    }
}
