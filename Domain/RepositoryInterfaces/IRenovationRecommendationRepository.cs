using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IRenovationRecommendationRepository
    {
        public List<RenovationRecommendation> GetAll();

        public RenovationRecommendation GetById(int id);

        public RenovationRecommendation Save(RenovationRecommendation renovationRecommendation);

        public int NextId();

        public void Delete(RenovationRecommendation renovationRecommendation);
        public RenovationRecommendation Update(RenovationRecommendation renovationRecommendation);


    }
}
