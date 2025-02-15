﻿using BookingApp.Domain.Model;
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
    internal class MyProfileViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public User LoggedInUser { get; set; }
        public List<TourInstance> TourInstances { get; set; }
        private string usernameText;
        public string UsernameText
        {
            get { return usernameText; }
            set
            {
                usernameText = value;
                OnPropertyChanged("UsernameText");
            }
        }
        private string guideText;
        public string GuideText
        {
            get { return guideText; }
            set
            {
                guideText = value;
                OnPropertyChanged("GuideText");
            }
        }
        private Visibility superG;
        public Visibility SuperG
        {
            get { return superG; }
            set
            {
                superG = value;
                OnPropertyChanged("SuperG");
            }
        }
        private ObservableCollection<string> superLanguages;
        public ObservableCollection<string> SuperLanguages 
        {
            get { return superLanguages; }
            set
            {
                superLanguages = value;
                OnPropertyChanged("SuperLanguages");
            }
        }
        public MyCommand QuitJob { get; set; }
        public bool Check {  get; set; }


        public MyProfileViewModel(User user, Action closeAction)
        {
            MainService = MainService.GetInstance();
            LoggedInUser = user;
            Check = false;
            QuitJob = new MyCommand(() =>
            {
                Quit();
                if (Check == true) { closeAction(); }
            });
            TourInstances = new List<TourInstance>(MainService.TourInstanceService.GetAll());
            LinkTourInstancesWithTours();
            UsernameText = LoggedInUser.Username;
            GuideText = "Guide";
            SuperG = Visibility.Hidden;
            CheckUsernameText(LoggedInUser);
            

        }

        private void CheckUsernameText(User LoggedInUser)
        {
            var langsInstances = MainService.TourInstanceService.GetLangsWithInstancesForSuperGuideCheck(LoggedInUser, TourInstances);
            SuperLanguages = new ObservableCollection<string>();
            if (langsInstances.Count != 0)
            {
                foreach(var kvp in langsInstances)
                {
                    string lang = kvp.Key;
                    List<TourInstance> instances = kvp.Value;
                    if (MainService.TourReviewService.SuperGuideGradeCheck(instances))
                    {
                        //JESTE SUPER GUIDE
                        SuperLanguages.Add(lang);
                        GuideText = "Super-guide";
                        SuperG = Visibility.Visible;
                    }
                }
            }
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
                List<TourInstance> tourInstances = new List<TourInstance>(MainService.TourInstanceService.GetByUserAndInFuture(LoggedInUser, TourInstances));
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
                MainService.UserService.Delete(LoggedInUser);
                Check = true;
                SignInForm signInForm = new SignInForm();
                signInForm.Show();
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
