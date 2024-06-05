using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IRenovationRecommendationService
    {

        List<RenovationRecommendation> GetAll();
        RenovationRecommendation Save(RenovationRecommendation recommendation);
        List<RenovationRecommendation> GetAllRecommendationByOwnerId(int ownerId);
        List<RenovationRecommendation> GetAllRecommendationsByAccommodationId(int accommodationId);
        void Delete(RenovationRecommendation recommendation);
    }
}
