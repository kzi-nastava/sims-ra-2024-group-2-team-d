using BookingApp.Domain.Model;
using BookingApp.WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class MoreInfoAboutTourViewModel: IRequestClose
    {
        public TourInstance TourInstance { get; set; }
        public ICommand OpenMorePicturesCommand { get; set; }

        public ICommand CloseCommand {  get; set; }

        public event EventHandler<DialogCloseRequestedEventArgs> RequestClose;
        public MoreInfoAboutTourViewModel(TourInstance tourInstance)
        {

            TourInstance = tourInstance;
            OpenMorePicturesCommand = new RelayCommand(OpenMorePictures);
            CloseCommand = new RelayCommand(Close);
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


    }
}
