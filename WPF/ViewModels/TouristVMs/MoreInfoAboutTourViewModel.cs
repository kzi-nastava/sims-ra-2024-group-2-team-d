using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.Services.IServices;
using BookingApp.WPF.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class MoreInfoAboutTourViewModel : IRequestClose, INotifyPropertyChanged
    {
        public TourInstance TourInstance { get; set; }

        private readonly ITourService _tourService;
        private readonly ITourInstanceService _tourInstanceService;
        private readonly IUserService _userService;
        public List<TourInstance> TourInstances { get; set; }

        public ICommand OpenMorePicturesCommand { get; set; }

        public ICommand CloseCommand { get; set; }

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

        public event EventHandler<DialogCloseRequestedEventArgs> RequestClose;
        public MoreInfoAboutTourViewModel(TourInstance tourInstance)
        {
            TourInstance = tourInstance;
            _tourService = Injector.Injector.CreateInstance<ITourService>();
            _tourInstanceService = Injector.Injector.CreateInstance<ITourInstanceService>();
            _userService =  Injector.Injector.CreateInstance<IUserService>();
            TourInstances = new List<TourInstance>(_tourInstanceService.GetAll());
            LinkTourInstancesWithTours();
            OpenMorePicturesCommand = new RelayCommand(OpenMorePictures);
            CloseCommand = new RelayCommand(Close);
            CheckIfGuidedBySuperGuide();
        }

        public void LinkTourInstancesWithTours()
        {
            foreach (TourInstance tourInstance in TourInstances)
            {
                Tour baseTour = _tourService.GetById(tourInstance.TourId);
                if (baseTour != null)
                {
                    tourInstance.BaseTour = baseTour;
                }
            }
        }

        public void Close()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(false));
        }

        public void OpenMorePictures()
        {
            ShowMorePicturesView view = new ShowMorePicturesView(TourInstance);
            view.Show();
        }

        public void CheckIfGuidedBySuperGuide()
        {
            if (_userService.GetAllSuperGuides(TourInstances).Find(c => c.Id == TourInstance.BaseTour.UserId) == null)
            {
                SuperG = Visibility.Hidden;
            }
            else { SuperG = Visibility.Visible; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}