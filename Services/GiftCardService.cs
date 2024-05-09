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
    public class GiftCardService
    {
        public GiftCardRepository GiftCardRepository { get; set; }

        public GiftCardService()
        {
            GiftCardRepository = new GiftCardRepository();
        }

        public GiftCard Save(GiftCard giftCard)
        {
            return GiftCardRepository.Save(giftCard);
        }
    }
}
