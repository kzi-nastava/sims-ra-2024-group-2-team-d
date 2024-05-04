using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourInstanceService
    {
        public TourInstanceRepository TourInstanceRepository { get; set; }

        public TourInstanceService()
        {
            TourInstanceRepository = new TourInstanceRepository();
        }

        public List<TourInstance> GetAll()
        {
            return TourInstanceRepository.GetAll();
        }

        public void Delete(TourInstance tourInstance)
        {
            TourInstanceRepository.Delete(tourInstance);
        }

        public TourInstance GetById(int id)
        {
            return TourInstanceRepository.GetById(id);
        }

        public List<TourInstance> GetByUser(User user, ObservableCollection<TourInstance> tours)
        {
            return tours.Where(c => c.BaseTour.UserId == user.Id).ToList();
        }

        public List<TourInstance> GetForTheDay1(User user, ObservableCollection<TourInstance> tours)
        {
            return tours.Where(c => c.BaseTour.UserId == user.Id && c.Date.Date == DateTime.Today).ToList();
        }
    }
}
