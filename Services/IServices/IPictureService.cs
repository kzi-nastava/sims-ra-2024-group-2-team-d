using BookingApp.Domain.Model;
using BookingApp.Repository;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IPictureService
    {
        public Picture Save(Picture picture);

        public List<string> GetByTourId(int tourId);
    }
}
