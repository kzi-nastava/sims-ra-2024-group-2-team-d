using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourService
    {
        public TourRepository TourRepository { get; set; }

        public TourService()
        {
            TourRepository = new TourRepository();
        }

        public Tour JustSave(Tour tour)
        {
            return TourRepository.JustSave(tour);
        }

        public Tour GetById(int id)
        {
            return TourRepository.GetById(id);
        }

    }
}
