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

namespace BookingApp.WPF.ViewModels.Guide
{
    public class AllToursViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public User LoggedInUser;
        public ObservableCollection<TourInstance> TourInstances { get; set; }
        private TourInstance tourInstance;
        public TourInstance SelectedTourInstance
        {
            get { return tourInstance; }
            set
            {
                tourInstance = value;
                OnPropertyChanged("SelectedTourInstance");
            }
        }
        public MyCommand DeleteCommand { get; set; }
        public MyCommand StatisticsCommand { get; set; }
        public MyCommand ReviewsCommand { get; set; }

        public AllToursViewModel(User user)
        {
            MainService = MainService.GetInstance();
            LoggedInUser = user;
            TourInstances = new ObservableCollection<TourInstance>(MainService.TourInstanceService.GetAll());
            LinkTourInstancesWithTours();
            TourInstances = new ObservableCollection<TourInstance>(MainService.TourInstanceService.GetByUser(user, TourInstances));
            DeleteCommand = new MyCommand(Delete);
            StatisticsCommand = new MyCommand(ShowStatistics);
            ReviewsCommand = new MyCommand(ShowReviews);

        }

        private void ShowReviews()
        {
            if (SelectedTourInstance != null)
            {
                if (SelectedTourInstance.End == true)
                {
                    GuideReviews guideReviews = new GuideReviews(SelectedTourInstance);
                    guideReviews.Show();
                }
                else
                {
                    MessageBox.Show("The tour is not finished!");
                }
            }
        }

        private void ShowStatistics()
        {
            if (SelectedTourInstance != null)
            {
                if (SelectedTourInstance.End == true)
                {
                    SpecificTourStatistics specificTourStatistics = new SpecificTourStatistics(SelectedTourInstance);
                    specificTourStatistics.Show();
                }
                else
                {
                    MessageBox.Show("The tour is not finished!");
                }
            }
        }

        private void Delete()
        {
            if (SelectedTourInstance != null)
            {
                if ((SelectedTourInstance.Date - DateTime.Now).TotalHours >= 48)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure?", "Cancel tour", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        MakeGiftCards1();
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("You can't cancel this tour", "Cancel tour", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void MakeGiftCards1()
        {
            int Id = SelectedTourInstance.Id;
            //_reservationRepository = new TourReservationRepository();
            List<int> users = new List<int>(MainService.TourReservationService.GetAllUsersByTourInstanceId(Id));
            foreach (int user in users)
            {
                GiftCard gc = new GiftCard(user);
                MainService.GiftCardService.Save(gc);
            }
            MainService.TourInstanceService.Delete(SelectedTourInstance);
            TourInstances.Remove(SelectedTourInstance);
        }

        public void LinkTourInstancesWithTours()
        {
            foreach (TourInstance tourInstance in TourInstances)
            {
                Tour baseTour = MainService.TourService.GetById(tourInstance.TourId);
                if (baseTour != null)
                {
                    tourInstance.BaseTour = baseTour;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
