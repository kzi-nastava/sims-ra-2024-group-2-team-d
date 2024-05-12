using BookingApp.Domain.Model;
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
        public TourRepository TourRepository { get; set; }

        private KeyPointRepository _keyPointRepository;

        private PictureRepository _pictureRepository;

        public TourInstanceService()
        {
            TourInstanceRepository = new TourInstanceRepository();
            TourRepository = new TourRepository();
            _keyPointRepository = new KeyPointRepository();
            _pictureRepository = new PictureRepository();
        }

        public List<TourInstance> GetAll()
        {
            return TourInstanceRepository.GetAll();
        }

        public void Delete(TourInstance tourInstance)
        {
            TourInstanceRepository.Delete(tourInstance);
        }

        public TourInstance Update(TourInstance tourInstance)
        {
            return TourInstanceRepository.Update(tourInstance);
        }

        public TourInstance Save(TourInstance tourInstance)
        {
            return TourInstanceRepository.Save(tourInstance);
        }

        public List<TourInstance> GetAllFinishedByUser(User user, ObservableCollection<TourInstance> tours)
        {
            return tours.Where(c => c.BaseTour.UserId == user.Id && c.End == true).ToList();
        }

        public bool CheckIfUserIsAvaliable(User user, DateTime dateTime, ObservableCollection<TourInstance> tourInstances)
        {
            if (tourInstances.Where(x => x.BaseTour.UserId == user.Id && x.Date == dateTime).ToList().Count != 0)
            {
                return false;
            }
            else return true;
        }

        public TourInstance GetById(int id)
        {
            TourInstance tourInstance = TourInstanceRepository.GetById(id);
            Tour tour = TourRepository.GetById(tourInstance.TourId);
            tourInstance.BaseTour = tour;
            tourInstance.BaseTour.KeyPoints = _keyPointRepository.GetByTourInstance(tourInstance);
            tourInstance.BaseTour.Pictures = _pictureRepository.GetByTourId(tourInstance.TourId);
            return tourInstance;
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
