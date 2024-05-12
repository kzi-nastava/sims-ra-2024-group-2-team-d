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
using System.Windows.Controls;
using System.Windows;
using BookingApp.View;
using System.Security.Cryptography.X509Certificates;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.WPF.ViewModels.Guide
{
    public class FollowTourViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public TourInstance TourInstance { get; set; }
        public static ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public static ObservableCollection<Tourist> Tourists { get; set; }
        public List<int> TouristsId { get; set; }
        public FollowingTourLive FollowingTourLive;
        private Tourist tourist;
        public Tourist SelectedTourist
        {
            get { return tourist; }
            set
            {
                tourist = value;
                OnPropertyChanged("SelectedTourist");
            }
        }
        public MyCommand EndTourCommand { get; set; }
        public MyCommand AddTouristsCommand { get; set; }
        public MyCommand EndInEmTourCommand { get; set; }
        private bool endEnabled;
        public bool EndEnabled
        {
            get { return endEnabled; }
            set
            {
                endEnabled = value;
                OnPropertyChanged(nameof(EndEnabled));
            }
        }
        private bool endInEmEnabled;
        public bool EndInEmEnabled
        {
            get { return endInEmEnabled; }
            set
            {
                endInEmEnabled = value;
                OnPropertyChanged(nameof(EndInEmEnabled));
            }
        }
        public FollowTour FollowTour { get; set; }

        public FollowTourViewModel(TourInstance tourInstance)
        {
            MainService = MainService.GetInstance();
            TourInstance = tourInstance;
            TourInstance.Start = true;
            MainService.TourInstanceService.Update(TourInstance);
            KeyPoints = new ObservableCollection<KeyPoint>(MainService.KeyPointService.GetByTourInstance(TourInstance));
            KeyPoints[0].Status = true;
            Tourists = new ObservableCollection<Tourist>(MainService.TourReservationService.GetAllTouristByTourId(TourInstance.Id));
            TouristsId = new List<int>();
            EndTourCommand = new MyCommand(EndTour);
            AddTouristsCommand = new MyCommand(AddTourists);
            EndInEmTourCommand = new MyCommand(EndInEmTour);
            EndEnabled = true;
            EndInEmEnabled = false;
            FollowTour = new FollowTour(TourInstance);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox == null)
                return;
            KeyPoint keyPoint = checkBox.DataContext as KeyPoint;

            if (keyPoint == null)
                return;

            keyPoint.Status = true;

            FollowingTourLive = new FollowingTourLive();
            FollowingTourLive.TourInstanceId = TourInstance.Id;
            FollowingTourLive.KeyPointId = keyPoint.Id;

            MainService.FollowingTourLiveService.Save(FollowingTourLive);

            if (AreAllCheckBoxesChecked())
            {
                EndEnabled = true;
                EndInEmEnabled = false;
            }
            else
            {
                EndEnabled = false;
                EndInEmEnabled = true;
            }
        }

        public bool AreAllCheckBoxesChecked()
        {
            return FollowTour.AreAllCheckBoxesChecked();
        }

        //public bool AreAllCheckBoxesChecked()
        //{
        //    foreach (var item in KeyPointGrid.ItemsSource)
        //    {
        //        var row = KeyPointGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
        //        if (row == null)
        //            continue;
        //        var checkBoxCell = KeyPointGrid.Columns[1].GetCellContent(row) as CheckBox;
        //        if (checkBoxCell == null || checkBoxCell.IsChecked != true)
        //            return false;
        //    }
        //    return true;
        //}

        public KeyPoint FindLastCheckedKeyPoint()
        {
            return FollowTour.FindLastCheckedKeyPoint();
        }

        //public KeyPoint FindLastCheckedKeyPoint()
        //{
        //    KeyPoint lastCheckedKeyPoint = null;

        //    foreach (var item in KeyPointGrid.ItemsSource)
        //    {
        //        var row = KeyPointGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
        //        if (row == null)
        //            continue;

        //        var checkBoxCell = KeyPointGrid.Columns[1].GetCellContent(row) as CheckBox;
        //        if (checkBoxCell == null || checkBoxCell.IsChecked != true)
        //            continue;

        //        KeyPoint keyPoint = row.Item as KeyPoint;
        //        lastCheckedKeyPoint = keyPoint;
        //    }
        //    return lastCheckedKeyPoint;
        //}

        private void EndTour()
        {
            TourInstance.End = true;
            MainService.TourInstanceService.Update(TourInstance);
            GuideWindow ft = new GuideWindow(MainService.UserService.GetById(TourInstance.BaseTour.UserId));
            ft.Show();
            //this.Close();
        }

        private void AddTourists()
        {
            Tourist ti = SelectedTourist;
            if (ti != null)
            {
                List<FollowingTourLive> toursLive = new List<FollowingTourLive>(MainService.FollowingTourLiveService.GetByTourInstanceId(TourInstance.Id));
                FollowingTourLive followingTourLive = toursLive.Find(r => r.KeyPointId == FindLastCheckedKeyPoint().Id);

                ti.ShowedUp = true;
                MainService.TouristService.Update(ti);
                followingTourLive.TouristsIds.Add(ti.Id);
                MainService.FollowingTourLiveService.Update(followingTourLive);
                TouristsId.Add(ti.Id);
                if (ti.UserId != -1)
                {
                    NotifyTouristUser(ti.UserId);
                }
            }
        }

        private void NotifyTouristUser(int userId)
        {
            LiveTourNotification liveTourNotification = new LiveTourNotification(TouristsId, TourInstance.Id);
            LiveTourNotificationService _liveTourNotificationService = new LiveTourNotificationService(Injector.Injector.CreateInstance<ILiveTourNotificationRepository>());
            LiveTourNotification savedNotification = _liveTourNotificationService.Save(liveTourNotification);
            TouristNotifications notification = new TouristNotifications(savedNotification.Id, "You have been added to the tour. Click \"See more\" to see other tourists", NotificationType.AddedToLiveTour, userId);
            TouristNotificationsService _touristNotificationsService = new TouristNotificationsService(Injector.Injector.CreateInstance<ITouristNotificationsRepository>(), Injector.Injector.CreateInstance<ITourCreationNotificationRepository>(), Injector.Injector.CreateInstance<ITourRequestRepository>());
            _touristNotificationsService.Save(notification);
        }

        private void EndInEmTour()
        {
            TourInstance.End = true;
            MainService.TourInstanceService.Update(TourInstance);
            GuideWindow ft = new GuideWindow(MainService.UserService.GetById(TourInstance.BaseTour.UserId));
            ft.Show();
            //this.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
