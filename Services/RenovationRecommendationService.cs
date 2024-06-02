using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;

namespace BookingApp.Services
{
    public class RenovationRecommendationService : IRenovationRecommendationService
    {
        public IRenovationRecommendationRepository _repository { get; set; }

        public RenovationRecommendationService()
        {

            _repository = Injector.Injector.CreateInstance<IRenovationRecommendationRepository>();
        }

        public void Save(RenovationRecommendation renovationRecommendation)
        {
            _repository.Save(renovationRecommendation);
        }
    }
}