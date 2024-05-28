using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class UserService
    {
        public IUserRepository UserRepository { get; set; }
        private ITourInstanceRepository TourInstanceRepository { get; set; }
        private ITourReviewRepository TourReviewRepository { get; set; }
        public TourInstanceService TourInstanceService { get; set; }
        public TourReviewService TourReviewService { get; set; }

        public UserService(IUserRepository userRepository, ITourInstanceRepository tourInstanceRepository, ITourRepository tourRepository, IKeyPointRepository keyPointRepository, IPictureRepository pictureRepository, ITourReviewRepository tourReviewRepository) 
        { 
            UserRepository = userRepository;
            //TourInstanceRepository = tourInstanceRepository;
            //TourReviewRepository = tourReviewRepository;
            TourInstanceService = new TourInstanceService(tourInstanceRepository, tourRepository, keyPointRepository, pictureRepository);
            TourReviewService = new TourReviewService(tourReviewRepository);
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
                if(CheckSuperGuide(user, TourInstances))
                {
                    superGuides.Add(user);
                }
            }
            return superGuides;
        }

    }
}
