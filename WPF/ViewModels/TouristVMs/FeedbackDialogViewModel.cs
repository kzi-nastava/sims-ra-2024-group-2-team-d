using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class FeedbackDialogViewModel: IRequestClose
    {
        public string Message { get; }

        public ICommand ConfirmCommand { get; }

        public event EventHandler<DialogCloseRequestedEventArgs> RequestClose;

        public FeedbackDialogViewModel(string message)
        {
            Message = message;
            ConfirmCommand = new RelayCommand(Confirm);
        }

        private void Confirm()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
