using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels
{
    public class ShowMorePicturesViewModel
    {
        public TourInstance TourInstance {  get; set; }
        public ShowMorePicturesViewModel(TourInstance tourInstance) {
            TourInstance = tourInstance;
        }

    }
}
