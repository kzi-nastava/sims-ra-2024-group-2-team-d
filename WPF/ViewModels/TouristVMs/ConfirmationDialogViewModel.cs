using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class ConfirmationDialogViewModel: IRequestClose, INotifyPropertyChanged
    {
        public string Message { get; }

        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        public event EventHandler<DialogCloseRequestedEventArgs> RequestClose;

        public ConfirmationDialogViewModel(string message)
        {
            Message = message;
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Confirm()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        private void Cancel()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(false));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
