using BookingApp.Domain.Model;
using BookingApp.Repository;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IGiftCardService
    {
        public GiftCard Save(GiftCard giftCard);

        public List<GiftCard> GetAll();
    }
}
