using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IAccommodationRepository
    {
        public List<Accommodation> getAccommodation();




        public List<Accommodation> GetAll();


        public Accommodation GetById(int id);


        public Accommodation Save(Accommodation accommodation);


        public int NextId();


        public void Delete(Accommodation accommodation);


        public Accommodation Update(Accommodation accommodation);

        //obrisi
        public List<Accommodation> GetAllOwnerAccommodations(int userId);

        public int getOwnerIdByAccommodationId(int accommodationId);


        public string getNameById(int accommodationId);
        
    }
}
