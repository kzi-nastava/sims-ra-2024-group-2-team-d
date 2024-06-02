using BookingApp.Domain.Model;

namespace BookingApp.Services.IServices
{
    public interface ITourService
    {
        public Tour JustSave(Tour tour);

        public Tour GetById(int id);
    }
}
