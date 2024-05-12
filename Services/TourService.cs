using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
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
        public ITourRepository TourRepository { get; set; }

        public TourService(ITourRepository tourRepository)
        {
            TourRepository = tourRepository;
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
