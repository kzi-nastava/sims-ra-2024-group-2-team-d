using BookingApp.Domain.Model;
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
        public PictureRepository PictureRepository { get; set; }

        public PictureService()
        {
            PictureRepository = new PictureRepository();
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
