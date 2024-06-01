using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class MoreInfoAboutTourViewModel: IRequestClose, INotifyPropertyChanged
    {
        public TourInstance TourInstance { get; set; }

        private readonly TourService _tourService;
        private readonly TourInstanceService _tourInstanceService;
        private readonly UserService _userService;
        public List<TourInstance> TourInstances { get; set; }

        public ICommand OpenMorePicturesCommand { get; set; }

        public ICommand CloseCommand {  get; set; }

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
        private readonly IDialogService _dialogService;


        public event EventHandler<DialogCloseRequestedEventArgs> RequestClose;
        public MoreInfoAboutTourViewModel(TourInstance tourInstance, IDialogService dialogService)
        {
            TourInstance = tourInstance;
            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            _tourInstanceService = new TourInstanceService(Injector.Injector.CreateInstance<ITourInstanceRepository>(), Injector.Injector.CreateInstance<ITourRepository>(), Injector.Injector.CreateInstance<IKeyPointRepository>(), Injector.Injector.CreateInstance<IPictureRepository>());
            _userService = new UserService(Injector.Injector.CreateInstance<IUserRepository>(), Injector.Injector.CreateInstance<ITourInstanceRepository>(), Injector.Injector.CreateInstance<ITourRepository>(), Injector.Injector.CreateInstance<IKeyPointRepository>(), Injector.Injector.CreateInstance<IPictureRepository>(), Injector.Injector.CreateInstance<ITourReviewRepository>());
            TourInstances = new List<TourInstance>(_tourInstanceService.GetAll());
            LinkTourInstancesWithTours();
            OpenMorePicturesCommand = new RelayCommand(OpenMorePictures);
            CloseCommand = new RelayCommand(Close);
            CheckIfGuidedBySuperGuide();
            _dialogService = dialogService;
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
            var viewModel = new ShowMorePicturesViewModel(TourInstance);
            bool? result = _dialogService.ShowDialog(viewModel);
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