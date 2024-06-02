using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;

namespace BookingApp.Services
{
    public class TourService : ITourService
    {
        public ITourRepository TourRepository { get; set; }

        public TourService()
        {
            TourRepository = Injector.Injector.CreateInstance<ITourRepository>();
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
