using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourInstanceRepository
    {
        public List<TourInstance> GetAll();

        public TourInstance Update(TourInstance tourInstance);

        public TourInstance UpdateFreeSpots(TourInstance tourInstance);

        public TourInstance UpdateReviewStatus(TourInstance tourInstance);

        public TourInstance GetById(int id);

        public TourInstance Save(TourInstance tourInstance);

        public int NextId();

        public void Delete(TourInstance tourInstance);

        public List<TourInstance> GetForTheDay1(User user, ObservableCollection<TourInstance> tours);

        public List<TourInstance> GetByUser(User user, ObservableCollection<TourInstance> tours);

        public List<TourInstance> GetAllFinishedByUser(User user, ObservableCollection<TourInstance> tours);

        public bool CheckIfUserIsAvaliable(User user, DateTime dateTime, ObservableCollection<TourInstance> tourInstances);

        public List<TourInstance> GetAllByIds(List<int> ids);

        public bool HasAtLeastFiveToursInLastYear(List<TourInstance> tourInstances);
    }
}
