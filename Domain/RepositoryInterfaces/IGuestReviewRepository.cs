using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IGuestReviewRepository
    {
        public List<GuestReview> GetAll();

        public GuestReview GetById(int id);

        public GuestReview Save(GuestReview guestReview);

        public int NextId();

        public void Delete(GuestReview guestReview);

        public GuestReview Update(GuestReview guestReview);
    }
}
