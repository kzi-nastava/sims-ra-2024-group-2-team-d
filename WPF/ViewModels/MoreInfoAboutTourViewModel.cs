using BookingApp.Model;
using BookingApp.WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels
{
    public class MoreInfoAboutTourViewModel
    {
        public TourInstance TourInstance { get; set; }
        public ICommand OpenMorePicturesCommand {  get; set; }
        public MoreInfoAboutTourViewModel(TourInstance tourInstance) { 

            TourInstance = tourInstance;
            OpenMorePicturesCommand = new RelayCommand(OpenMorePictures);

        }

        public void OpenMorePictures()
        {
            ShowMorePicturesView view = new ShowMorePicturesView(TourInstance);
            view.Show();
        }


    }
}
