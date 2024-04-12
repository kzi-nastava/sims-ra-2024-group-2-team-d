using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels
{
    public class UserToursViewModel
    {
        public static ObservableCollection<TourInstance> ReservedTours { get; set; }
        public static ObservableCollection<TourInstance> FinishedTours { get; set; }

        public static TourInstance SelectedTour { get; set; }
       public UserToursViewModel(ObservableCollection<TourInstance> userTours) {
            ReservedTours = new ObservableCollection<TourInstance>();
            FinishedTours = new ObservableCollection<TourInstance>();
            FilterTours(userTours);
        }

        public void FilterTours(ObservableCollection<TourInstance> userTours)
        {
            foreach(TourInstance tourInstance in userTours)
            {
                if (tourInstance.End)
                {
                    FinishedTours.Add(tourInstance);
                }else if (!tourInstance.Start)
                {
                    ReservedTours.Add(tourInstance);
                }
            }
        }
    }
}
