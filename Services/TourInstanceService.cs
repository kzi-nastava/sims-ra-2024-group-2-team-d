using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BookingApp.Services
{
    public class TourInstanceService : ITourInstanceService
    {
        private ITourInstanceRepository TourInstanceRepository { get; set; }
        private ITourRepository TourRepository { get; set; }

        private IKeyPointRepository _keyPointRepository { get; set; }

        private IPictureRepository _pictureRepository { get; set; }

        public TourInstanceService()
        {
            TourInstanceRepository = Injector.Injector.CreateInstance<ITourInstanceRepository>();
            TourRepository = Injector.Injector.CreateInstance<ITourRepository>();
            _keyPointRepository = Injector.Injector.CreateInstance<IKeyPointRepository>();
            _pictureRepository = Injector.Injector.CreateInstance<IPictureRepository>();
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

        public List<TourInstance> GetByUserAndInFuture(User user, List<TourInstance> tours)
        {
            return tours.Where(c => c.BaseTour.UserId == user.Id && c.Date.Date > DateTime.Today).ToList();
        }

        public Dictionary<string, List<TourInstance>> GetLangsWithInstancesForSuperGuideCheck(User user, List<TourInstance> tourInstances)
        {
            List<TourInstance> tours = tourInstances.Where(c => c.BaseTour.UserId == user.Id && c.End == true && c.Date.Date >= DateTime.Today.AddYears(-1)).ToList();
            Dictionary<string, List<TourInstance>> languageCounts = new Dictionary<string, List<TourInstance>>();
            foreach (var instance in tours)
            {
                if (languageCounts.ContainsKey(instance.BaseTour.Language))
                {
                    languageCounts[instance.BaseTour.Language].Add(instance);
                }
                else
                {
                    languageCounts[instance.BaseTour.Language] = new List<TourInstance> { instance };
                }
            }

            return languageCounts.Where(kvp => kvp.Value.Count >= 20).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }


        public List<TourInstance> GetForTheDay1(User user, ObservableCollection<TourInstance> tours)
        {
            return tours.Where(c => c.BaseTour.UserId == user.Id && c.Date.Date == DateTime.Today).ToList();
        }
    }
}
