using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;
using System.Collections.Generic;

namespace BookingApp.Services
{
    public class RenovationRecommendationService : IRenovationRecommendationService
    {
        public IRenovationRecommendationRepository _repository { get; set; }

        public RenovationRecommendationService()
        {

            _repository = Injector.Injector.CreateInstance<IRenovationRecommendationRepository>();
        }

        public void Delete(RenovationRecommendation recommendation)
        {
            _repository.Delete(recommendation);
        }

        public List<RenovationRecommendation> GetAll()
        {
            return _repository.GetAll();
        }

        public List<RenovationRecommendation> GetAllRecommendationByOwnerId(int ownerId)
        {
            return _repository.GetAllRecommendationByOwnerId(ownerId);
        }

        public List<RenovationRecommendation> GetAllRecommendationsByAccommodationId(int accommodationId)
        {
            return _repository.GetAllRecommendationsByAccommodationId(accommodationId);
        }

        public RenovationRecommendation Save(RenovationRecommendation recommendation)
        {
            return _repository.Save(recommendation);
        }
    }
}