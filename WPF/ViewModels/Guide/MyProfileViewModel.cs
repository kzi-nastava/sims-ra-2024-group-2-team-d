using BookingApp.Domain.Model;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModels.Guide
{
    internal class MyProfileViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public User LoggedInUser { get; set; }
        public List<TourInstance> TourInstances { get; set; }
        public MyCommand QuitJob { get; set; }


        public MyProfileViewModel(User user)
        {
            MainService = MainService.GetInstance();
            LoggedInUser = user;
            QuitJob = new MyCommand(Quit);
            TourInstances = new List<TourInstance>(MainService.TourInstanceService.GetAll());
            LinkTourInstancesWithTours();

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

        public void Quit()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Quit job",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                List<TourInstance> tourInstances = new List<TourInstance>(MainService.TourInstanceService.GetByUserInFuture(LoggedInUser, TourInstances));
                foreach (TourInstance instance in tourInstances)
                {
                    if (instance != null)
                    {
                        List<int> users = new List<int>(MainService.TourReservationService.GetAllUsersByTourInstanceId(instance.Id));
                        foreach (int user in users)
                        {
                            GiftCard gc = new GiftCard(user);
                            gc.ExpirationDate = DateOnly.FromDateTime(DateTime.Now).AddYears(2);
                            MainService.GiftCardService.Save(gc);
                        }
                        MainService.TourInstanceService.Delete(instance);
                        TourInstances.Remove(instance);
                    }
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
