using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class AccomodationService
    {
        public AccommodationRepository _repo { get; set; }

        public AccomodationService() {

            _repo = new AccommodationRepository();
        }



        public List<Accommodation> GetAllOwnerAccommodations(int userId)
        {
            return _repo.GetAll().Where(a => a.UserId == userId).ToList();
        }

        public void Delete(Accommodation accommodation)
        {
            _repo.Delete(accommodation);
        }

        public int getOwnerIdByAccommodationId(int accommodationId)
        {
            return _repo.getAccommodation().Find(a => a.Id == accommodationId).UserId;
        }

        public string getNameById(int accommodationId)
        {
            return _repo.getAccommodation().Find(a => a.Id == accommodationId).Name;
        }
    }
}
