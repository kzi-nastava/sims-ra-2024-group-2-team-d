using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using BookingApp.Services.IServices;
using System.Collections.Generic;

namespace BookingApp.Services
{
    public class UserService : IUserService
    {
        public IUserRepository UserRepository { get; set; }
        private ITourInstanceRepository TourInstanceRepository { get; set; }
        private ITourReviewRepository TourReviewRepository { get; set; }
        public ITourInstanceService TourInstanceService { get; set; }
        public ITourReviewService TourReviewService { get; set; }

        public UserService()
        {
            UserRepository = Injector.Injector.CreateInstance<IUserRepository>(); ;
            //TourInstanceRepository = tourInstanceRepository;
            //TourReviewRepository = tourReviewRepository;
            TourInstanceService = Injector.Injector.CreateInstance<ITourInstanceService>(); ;
            TourReviewService = Injector.Injector.CreateInstance<ITourReviewService>(); ;
        }

        public User GetById(int id)
        {
            return UserRepository.GetById(id);
        }

        public List<User> GetAll()
        {
            return UserRepository.GetAll();
        }

        public bool CheckSuperGuide(User user, List<TourInstance> TourInstances)
        {
            bool superGuide = false;
            var langsInstances = TourInstanceService.GetLangsWithInstancesForSuperGuideCheck(user, TourInstances);
            if (langsInstances.Count != 0)
            {
                foreach (var kvp in langsInstances)
                {
                    string lang = kvp.Key;
                    List<TourInstance> instances = kvp.Value;
                    if (TourReviewService.SuperGuideGradeCheck(instances))
                    {
                        superGuide = true;
                    }
                }
            }
            return superGuide;
        }

        public List<User> GetAllSuperGuides(List<TourInstance> TourInstances)
        {
            List<User> users = GetAll();
            List<User> superGuides = new List<User>();
            foreach (var user in users)
            {
                if (CheckSuperGuide(user, TourInstances))
                {
                    superGuides.Add(user);
                }
            }
            return superGuides;
        }

        public void Delete(User user)
        {
            UserRepository.Delete(user);
        }

    }
}
