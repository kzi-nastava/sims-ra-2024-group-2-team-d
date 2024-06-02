using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;
using System.Collections.Generic;

namespace BookingApp.Services
{
    public class PictureService : IPictureService
    {
        private IPictureRepository PictureRepository { get; set; }

        public PictureService()
        {
            PictureRepository =  Injector.Injector.CreateInstance<IPictureRepository>();
        }

        public Picture Save(Picture picture)
        {
            return PictureRepository.Save(picture);
        }

        public List<string> GetByTourId(int tourId)
        {
            return PictureRepository.GetByTourId(tourId);
        }

    }
}
