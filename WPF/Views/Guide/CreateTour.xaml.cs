using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.View.Owner;
using BookingApp.WPF.ViewModels.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Window //, INotifyPropertyChanged
    {
        //// public User LoggedInUser { get; set; }
        ////public Tour SelectedTour { get; set; }

        //private readonly TourRepository _repository;
        //private readonly TourInstanceRepository _instanceRepository;
        //private readonly KeyPointRepository _keyPointRepository;
        //private readonly PictureRepository _pictureRepository;



        //public User LoggedInUser { get; set; }

        //// public Tour SelectedTour { get; set; }

        //public KeyPoint KeyPoint { get; set; }
        //public List<KeyPoint> KeyPoints { get; set; }

        //public TourInstance TourInstance { get; set; }
        //public List<TourInstance> TourInstances { get; set; }

        //public Picture Picture { get; set; }
        //public List<Picture> Pictures { get; set; }

        //public static ObservableCollection<TourInstance> TourInstancesColl { get; set; }
        //public static ObservableCollection<Tour> BaseTours { get; set; }


        //public TourDto Tour { get; set; }

        public CreateTour(User user, string loc, string lang)
        {
            InitializeComponent();
            DataContext = new CreateTourViewModel(user, loc, lang);
            //Title = "Create Tour";
            //DataContext = this;
            //LoggedInUser = user;
            //_repository = new TourRepository();
            //_instanceRepository = new TourInstanceRepository();
            //_keyPointRepository = new KeyPointRepository();
            //_pictureRepository = new PictureRepository();


            //Tour = new TourDto();
            //Tour.UserId = user.Id;

            //TourInstance = new TourInstance();
            //KeyPoint = new KeyPoint();
            //Picture = new Picture();

            //// CheckReviewNotifications();

        }

        public CreateTour(User user, string loc, string lang, TourCreationNotification notification)
        {
            InitializeComponent();
            DataContext = new CreateTourViewModel(user, loc, lang, notification);

        }

        //private void CreateNewTour(object sender, RoutedEventArgs e)
        //{
        //    string validTour = Tour.IsValid;

        //    if (validTour == string.Empty)
        //    {
        //        Tour newTour = Tour.ToModel();
        //        Tour newT = _repository.JustSave(newTour);
        //        List<TourInstance> newTourInstances = new List<TourInstance>();
        //        newTourInstances = newT.TourInstances;
        //        foreach (TourInstance instance in newTourInstances)
        //        {
        //            if (_repository.GetById(instance.TourId) != null)
        //            {
        //                instance.BaseTour = _repository.GetById(instance.TourId);
        //            }

        //            _instanceRepository.Save(instance);
        //        }

        //        List<Picture> newPictures = new List<Picture>();
        //        newPictures = newT.ClassPictures;
        //        foreach (Picture picture in newPictures)
        //        {
        //            _pictureRepository.Save(picture);
        //        }

        //        List<KeyPoint> newKeyPoints = new List<KeyPoint>();
        //        newKeyPoints = newT.KeyPoints;
        //        foreach (KeyPoint keyPoint in newKeyPoints)
        //        {
        //            _keyPointRepository.Save(keyPoint);
        //        }
        //        /* TourInstancesColl = new ObservableCollection<TourInstance>();

        //         LinkTourInstancesWithTours();*/

        //        this.Close();
        //    }
        //    else
        //    {
        //        ErrorMessage.Visibility = Visibility.Visible;
        //        ErrorMessage.Content = validTour;
        //    }
        //}

        ///* public void LinkTourInstancesWithTours()
        // {
        //     foreach (TourInstance tourInstance in TourInstances)
        //     {
        //         if (_repository.GetById(tourInstance.TourId) != null)
        //         {
        //             tourInstance.BaseTour = _repository.GetById(tourInstance.TourId);
        //         }
        //     }
        // }*/


        ///*  private void SaveComment(object sender, RoutedEventArgs e)
        //  { 
        //      Comment newComment = new Comment(DateTime.Now, Text, LoggedInUser);
        //      Comment savedComment = _repository.Save(newComment);
        //      CommentsOverview.Comments.Add(savedComment);

        //      Close();
        //  }*/


        //private void Cancel(object sender, RoutedEventArgs e)
        //{
        //    Close();
        //}


        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}


        ///* private void CheckReviewNotifications()
        // {
        //     var list = _accommodationRepository.GetAllOwnerAccommodations(_user.Id).Select(a => a.Id).ToList();
        //     foreach (var r in _reservationRepository.GetAllUnreviewed(list))
        //     {
        //         GuestReviewForm guestReviewForm = new GuestReviewForm(r);
        //         guestReviewForm.Show();
        //     }

        // }*/
    }
}
