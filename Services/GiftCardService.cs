using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;
using System.Collections.Generic;

namespace BookingApp.Services
{
    public class GiftCardService : IGiftCardService
    {
        public IGiftCardRepository GiftCardRepository { get; set; }

        public GiftCardService()
        {
            GiftCardRepository = Injector.Injector.CreateInstance<IGiftCardRepository>();
        }

        public GiftCard Save(GiftCard giftCard)
        {
            return GiftCardRepository.Save(giftCard);
        }

        public List<GiftCard> GetAll()
        {
            return GiftCardRepository.GetAll();
        }
    }
}
