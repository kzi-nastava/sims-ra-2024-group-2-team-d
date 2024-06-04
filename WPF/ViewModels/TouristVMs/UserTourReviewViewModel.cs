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
        public TourInstance TourInstanceForReview { get; set; }

        public UserTourReviewViewModel(User loggedInUser, TourInstance tourInstance, MainViewModel mainViewModel, IDialogService dialogService, ObservableCollection<TourInstance> tourInstances)
        {
            UserTourReview = new TourReview();
            UserTourReview.TourInstanceId = tourInstance.Id;
            UserTourReview.GuideId = tourInstance.BaseTour.UserId;
            UserTourReview.UserId = loggedInUser.Id;
            _pictureService = Injector.Injector.CreateInstance<IPictureService>();
            ConfirmReviewCommand = new RelayCommand(ConfirmReview);
            ImagePaths = new ObservableCollection<string>();
            AddImageCommand = new RelayCommand(AddImageExecute);
            TourTitle = tourInstance.BaseTour.Title;
            ChosenDate = tourInstance.Date;
            MainViewModel = mainViewModel;
            _dialogService = dialogService;
            GoBackCommand = new RelayCommand(GoBack);
            TourInstances = tourInstances;
            ShowAllAddedPicturesCommand = new RelayCommand(ShowAllAddedPictures);
            LoggedInUser = loggedInUser;
            TourInstanceForReview = tourInstance;
        }

        public void ShowAllAddedPictures()
        {
            var viewModel = new ShowAllAddedPicturesForTourReviewViewModel(ImagePaths);
            bool? result = _dialogService.ShowDialog(viewModel);
        }
        public void GoBack()
        {
            var confirmationViewModel = new ConfirmationDialogViewModel("Are you sure you want to exit?");
            bool? result = _dialogService.ShowDialog(confirmationViewModel);
            if (result == true)
            {
                MainViewModel.SwitchView(new UserToursViewModel(LoggedInUser, TourInstances, _dialogService, MainViewModel));
            }
        }
        public void ConfirmReview()
        {
            if(UserTourReview.Enjoyability == 0 || UserTourReview.GuideKnowledge == 0 || UserTourReview.GuideLanguage == 0)
            {
                var viewModel = new FeedbackDialogViewModel("All scores are required!");
                bool? feedbackResult = _dialogService.ShowDialog(viewModel);
                return;
            }
            var confirmationViewModel = new ConfirmationDialogViewModel("Are you sure you want to submit this review?");
            bool? result = _dialogService.ShowDialog(confirmationViewModel);
            if(result == true)
            {              
                var _tourReviewService = Injector.Injector.CreateInstance<ITourReviewService>();
                _tourReviewService.Save(UserTourReview);
                foreach (string imagePath in ImagePaths)
                {
                    Picture picture = new Picture(TourInstanceForReview.BaseTour.Id, imagePath);
                    _pictureService.Save(picture);
                }

                TourInstanceForReview.IsNotReviewed = false;
                TourInstanceService _tourInstanceRepository = new TourInstanceService();
                _tourInstanceRepository.UpdateReviewStatus(TourInstanceForReview);
                var feedbackViewModel = new FeedbackDialogViewModel("Review has been sumbited!");
                bool? feedbackResult = _dialogService.ShowDialog(feedbackViewModel);
                MainViewModel.SwitchView(new UserToursViewModel(LoggedInUser, TourInstances, _dialogService, MainViewModel));
            }
            
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
