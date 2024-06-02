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

        public string TourTitle {  get; set; }

        public DateTime ChosenDate {  get; set; }
        public ICommand GoBackCommand {  get; set; }

        public ObservableCollection<int> Ratings { get; } = new ObservableCollection<int>() { 1, 2, 3, 4, 5 };

        private readonly MainViewModel MainViewModel;
        public IDialogService _dialogService { get; set; }
        public User LoggedInUser {  get; set; }

        public ObservableCollection<TourInstance> TourInstances { get; set; }

        public ICommand ShowAllAddedPicturesCommand {  get; set; }

        public UserTourReviewViewModel(User loggedInUser, TourInstance tourInstance, MainViewModel mainViewModel, IDialogService dialogService, ObservableCollection<TourInstance> tourInstances)
        {
            UserTourReview = new TourReview();
            UserTourReview.TourInstanceId = tourInstance.Id;
            UserTourReview.GuideId = tourInstance.BaseTour.UserId;
            UserTourReview.UserId = loggedInUser.Id;
            _pictureService = Injector.Injector.CreateInstance<IPictureService>();
            ConfirmReviewCommand = new RelayCommand(() =>
            {
                ConfirmReview(tourInstance);
            });
            ImagePaths = new ObservableCollection<string>();
            AddImageCommand = new RelayCommand(AddImageExecute);
            TourTitle = tourInstance.BaseTour.Title;
            ChosenDate = tourInstance.Date;
            MainViewModel = mainViewModel;
            _dialogService = dialogService;
            GoBackCommand = new RelayCommand(GoBack);
            TourInstances = tourInstances;
            ShowAllAddedPicturesCommand = new RelayCommand(ShowAllAddedPictures);
        }

        public void ShowAllAddedPictures()
        {
            var viewModel = new ShowAllAddedPicturesForTourReviewViewModel(ImagePaths);
            bool? result = _dialogService.ShowDialog(viewModel);
        }
        public void GoBack()
        {
            MainViewModel.SwitchView(new UserToursViewModel(LoggedInUser, TourInstances, _dialogService, MainViewModel));
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
