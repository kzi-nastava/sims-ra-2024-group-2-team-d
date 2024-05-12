using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.View;
using InitialProject.Presentation.WPF.ViewModel;
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
    public class GuideWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TourInstance> TourInstances { get; set; }
        private MainService MainService { get; set; }
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
        public User LoggedInUser;
        public MyCommand ShowCreateTour { get; set; }
        public MyCommand ShowStartTour { get; set; }
        public MyCommand ShowAllTours { get; set; }
        public MyCommand ShowStatistics { get; set; }
        public MyCommand ShowLogOut { get; set; }
        public MyCommand ShowRequests { get; set; }

        public GuideWindowViewModel(User loggedInUser, Action closeAction)
        {
            MainService = MainService.GetInstance();
            LoggedInUser = loggedInUser;
            TourInstances = new ObservableCollection<TourInstance>(MainService.TourInstanceService.GetAll());
            LinkTourInstancesWithTours();
            TourInstances = new ObservableCollection<TourInstance>(MainService.TourInstanceService.GetForTheDay1(LoggedInUser, TourInstances));
            ShowCreateTour = new MyCommand(ShowCreateTourForm);
            ShowStartTour = new MyCommand(StartTour);
            ShowAllTours = new MyCommand(AllTours);
            ShowStatistics = new MyCommand(Statistics);
            ShowLogOut = new MyCommand(() =>
            {
                if (LogOut())
                {
                    closeAction();
                }
            });
            ShowRequests = new MyCommand(Requests);
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

        private void Requests()
        {
            RequestsGuide requestsGuide = new RequestsGuide(LoggedInUser);
            requestsGuide.Show();
        }

        private bool LogOut()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Log out",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SignInForm signInForm = new SignInForm();
                signInForm.Show();
                return true;
                //this.Close();
            }
            else { return false; }
        }

        private void Statistics()
        {
            Statistics statistics = new Statistics(LoggedInUser);
            statistics.Show();
        }

        private void AllTours()
        {
            AllTours allTours = new AllTours(LoggedInUser);
            allTours.Show();
        }

        private void ShowCreateTourForm()
        {
            CreateTour createTour = new CreateTour(LoggedInUser, "", "");
            createTour.Show();
        }

        private void StartTour()
        {
            if (SelectedTourInstance != null && SelectedTourInstance.End != true)
            {
                FollowTour ft = new FollowTour(SelectedTourInstance);
                ft.Show();
                //Close();
            }
        }
    }
}
