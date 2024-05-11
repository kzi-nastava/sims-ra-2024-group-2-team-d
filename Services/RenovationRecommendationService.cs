using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.Repository;

namespace BookingApp.Services
{
    public class RenovationRecommendationService
    {
        public RenovationRecommendationRepository _repository { get; set; }

        public RenovationRecommendationService()
        {

            _repository = new RenovationRecommendationRepository();
        }

        public void Save(RenovationRecommendation renovationRecommendation)
        {
            _repository.Save(renovationRecommendation);
        }
    }
}