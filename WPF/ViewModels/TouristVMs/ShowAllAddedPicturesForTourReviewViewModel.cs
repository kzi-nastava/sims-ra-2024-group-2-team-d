using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class ShowAllAddedPicturesForTourReviewViewModel: IRequestClose
    {
        public ObservableCollection<string> ImagePaths { get; set; }
        public event EventHandler<DialogCloseRequestedEventArgs> RequestClose;
        public ICommand CloseCommand { get; set; }

        public ShowAllAddedPicturesForTourReviewViewModel(ObservableCollection<string> imagePaths) { 
            ImagePaths = imagePaths;
            CloseCommand = new RelayCommand(Close);

        }

        public void Close()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(true));

        }
    }
}
