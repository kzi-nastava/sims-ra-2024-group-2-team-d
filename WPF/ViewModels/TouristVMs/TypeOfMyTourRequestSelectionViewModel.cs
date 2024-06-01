using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookingApp.Domain.Model;
using BookingApp.WPF.Views;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class TypeOfMyTourRequestSelectionViewModel: IRequestClose, INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }

        public ICommand ShowMyStandardTourRequestsCommand { get; set; }

        public ICommand ShowMyComplexTourRequestsCommand { get; set; }


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

        private readonly MainViewModel _mainViewModel;

        public ICommand BackButtonCommand { get; set; }
        public TypeOfMyTourRequestSelectionViewModel(MainViewModel mainViewModel,User loggedInUser)
        {
            LoggedInUser = loggedInUser;
            ShowMyStandardTourRequestsCommand = new RelayCommand(ShowMyStandardTourRequests);
            _mainViewModel = mainViewModel;
            BackButtonCommand = new RelayCommand(GoBack);
            ShowMyComplexTourRequestsCommand = new RelayCommand(ShowMyComplexTourRequests);
        }

        public void GoBack()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(false));
        }
        public void ShowMyStandardTourRequests()
        {
            //MyStandardTourRequestsView view = new MyStandardTourRequestsView(LoggedInUser);
            //view.Show();
            SelectedOption = "Standard";
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(true, "Standard"));
        }

        public void ShowMyComplexTourRequests()
        {
            SelectedOption = "Complex";
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(true, "Complex"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
