using BookingApp.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class UserTourReviewViewModel
    {
        public TourReview UserTourReview { get; set; }

        public ICommand ConfirmReviewCommand { get; set; }

        public ObservableCollection<string> ImagePaths { get; }

        public ICommand AddImageCommand { get; }

        private IPictureService _pictureService;

        public ObservableCollection<int> Ratings { get; } = new ObservableCollection<int>() { 1, 2, 3, 4, 5 };

        public UserTourReviewViewModel(User loggedInUser, TourInstance tourInstance, Action closeAction)
        {
            UserTourReview = new TourReview();
            UserTourReview.TourInstanceId = tourInstance.Id;
            UserTourReview.GuideId = tourInstance.BaseTour.UserId;
            UserTourReview.UserId = loggedInUser.Id;
            _pictureService = Injector.Injector.CreateInstance<IPictureService>();
            ConfirmReviewCommand = new RelayCommand(() =>
            {
                ConfirmReview(tourInstance);
                closeAction();
            });
            ImagePaths = new ObservableCollection<string>();
            AddImageCommand = new RelayCommand(AddImageExecute);
        }

        public void ConfirmReview(TourInstance tourInstance)
        {
            var _tourReviewService = Injector.Injector.CreateInstance<ITourReviewService>();
            _tourReviewService.Save(UserTourReview);
            foreach (string imagePath in ImagePaths)
            {
                Picture picture = new Picture(tourInstance.BaseTour.Id, imagePath);
                _pictureService.Save(picture);
            }
            /*
            tourInstance.IsNotReviewed = false;
            TourInstanceService _tourInstanceRepository = new TourInstanceService();
            _tourInstanceRepository.UpdateReviewStatus(tourInstance);      */
        }

        private void AddImageExecute()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                foreach (string fullFilename in openFileDialog.FileNames)
                {
                    string relativePath = Path.GetRelativePath(baseDirectory, fullFilename);
                    ImagePaths.Add(relativePath);
                }
            }
        }

    }
}
