using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookingApp.WPF.Views;
using BookingApp.Domain.Model;
using System.ComponentModel;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class TypeOfTourRequestSelectionViewModel: IRequestClose, INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }
        public ICommand StandardTourRequestCreationCommand { get; set; }
        public event EventHandler<DialogCloseRequestedEventArgs> RequestClose;
        private string _selectedOption;
        public string SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged(nameof(SelectedOption));
            }
        }
        public ICommand BackButtonCommand { get; set; }

        public TypeOfTourRequestSelectionViewModel(User loggedInUser)
        {
            StandardTourRequestCreationCommand = new RelayCommand(OpenStandardTourCreationWindow);
            LoggedInUser = loggedInUser;
            BackButtonCommand = new RelayCommand(GoBack);
        }

        public void GoBack()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(false));
        }

        public void OpenStandardTourCreationWindow()
        {
            SelectedOption = "Standard";
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(true, "Standard"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
