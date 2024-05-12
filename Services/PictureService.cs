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
    public class PictureService
    {
        private IPictureRepository PictureRepository { get; set; }

        public PictureService(IPictureRepository pictureRepository)
        {
            PictureRepository = pictureRepository;
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
