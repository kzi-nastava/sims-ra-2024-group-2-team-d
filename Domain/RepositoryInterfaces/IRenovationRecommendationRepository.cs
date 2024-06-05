using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IRenovationRecommendationRepository
    {
        List<RenovationRecommendation> GetAll();
        RenovationRecommendation Save(RenovationRecommendation recommendation);
        List<RenovationRecommendation> GetAllRecommendationByOwnerId(int ownerId);
        List<RenovationRecommendation> GetAllRecommendationsByAccommodationId(int accommodationId);
        int NextId();
        void Delete(RenovationRecommendation recommendation);


    }
}
