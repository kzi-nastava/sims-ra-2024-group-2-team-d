using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.IServices
{
    public interface ITourInstanceService
    {
        List<TourInstance> GetAll();


        void Delete(TourInstance tourInstance);

        TourInstance Update(TourInstance tourInstance);

        TourInstance Save(TourInstance tourInstance);

        List<TourInstance> GetAllFinishedByUser(User user, ObservableCollection<TourInstance> tours);

        bool CheckIfUserIsAvaliable(User user, DateTime dateTime, ObservableCollection<TourInstance> tourInstances);

        TourInstance GetById(int id);

        List<TourInstance> GetByUser(User user, ObservableCollection<TourInstance> tours);

        List<TourInstance> GetByUserAndInFuture(User user, List<TourInstance> tours);

        Dictionary<string, List<TourInstance>> GetLangsWithInstancesForSuperGuideCheck(User user, List<TourInstance> tourInstances);


        List<TourInstance> GetForTheDay1(User user, ObservableCollection<TourInstance> tours);
        List<DateTime> FindAllUnavaliableDates(User user, ObservableCollection<TourInstance> tours);

        public TourInstance UpdateFreeSpots(TourInstance tourInstance);
        public List<TourInstance> GetAllForReservation();


    }
}
