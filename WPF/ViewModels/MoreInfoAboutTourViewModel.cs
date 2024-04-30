using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels
{
    public class MoreInfoAboutTourViewModel
    {
        public TourInstance TourInstance { get; set; }
        public MoreInfoAboutTourViewModel(TourInstance tourInstance) { 

            TourInstance = tourInstance;

        }


    }
}
