using BookingApp.Model;
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

namespace BookingApp.WPF.ViewModels
{
    public class UserTourReviewViewModel
    {
        public TourReview UserTourReview { get; set; }

        private TourReviewRepository _tourReviewRepository;

        public ICommand ConfirmReviewCommand {  get; set; }

        public ObservableCollection<string> ImagePaths { get; }

        public ICommand AddImageCommand { get; }

        private PictureRepository _pictureRepository;

        public UserTourReviewViewModel(User loggedInUser, TourInstance tourInstance, Action closeAction)
        {
            UserTourReview = new TourReview();
            UserTourReview.TourInstanceId = tourInstance.Id;
            UserTourReview.GuideId = tourInstance.BaseTour.UserId;
            UserTourReview.UserId = loggedInUser.Id;
            _pictureRepository = new PictureRepository();
            ConfirmReviewCommand = new RelayCommand(() =>
            {
                ConfirmReview(tourInstance.BaseTour.Id);
                closeAction();
            });
            ImagePaths = new ObservableCollection<string>();
            AddImageCommand = new RelayCommand(AddImageExecute);

        }

        public void ConfirmReview(int tourId)
        {
            
            TourReviewRepository _tourReviewRepository = new TourReviewRepository();
            _tourReviewRepository.Save(UserTourReview);
            foreach(string imagePath in ImagePaths)
            {
                Picture picture = new Picture(tourId,imagePath);
                _pictureRepository.Save(picture);
            }

            
        }

        private void AddImageExecute()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;  // ili Environment.CurrentDirectory
                foreach (string fullFilename in openFileDialog.FileNames)
                {
                    // Kreiranje relativne putanje
                    string relativePath = Path.GetRelativePath(baseDirectory, fullFilename);
                    ImagePaths.Add(relativePath);
                }
            }
        }

    }
}
