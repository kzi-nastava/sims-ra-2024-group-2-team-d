using BookingApp.Domain.Model;
using BookingApp.WPF.Views.TouristV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        private IDialogService _dialogService;

        public MainViewModel(User loggedInUser)
        {
            // Postavite početni ViewModel
            _dialogService = new DialogService();
            CurrentViewModel = new TouristHomeViewModel(this, loggedInUser, _dialogService);
        }

        public void SwitchView(object viewModel)
        {
            CurrentViewModel = viewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
