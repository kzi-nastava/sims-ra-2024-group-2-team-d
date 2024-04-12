using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class GiftCardRepository
    {
        private const string FilePath = "../../../Resources/Data/giftCards.csv";

        private readonly Serializer<GiftCard> _serializer;

        private List<GiftCard> _giftCards;

        public GiftCardRepository()
        {
            _serializer = new Serializer<GiftCard>();
            _giftCards = _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _giftCards = _serializer.FromCSV(FilePath);
            if (_giftCards.Count < 1)
            {
                return 1;
            }
            return _giftCards.Max(c => c.Id) + 1;
        }

        public GiftCard Save(GiftCard giftCard)
        {
            giftCard.Id = NextId();
            _giftCards = _serializer.FromCSV(FilePath);
            _giftCards.Add(giftCard);
            _serializer.ToCSV(FilePath, _giftCards);
            return giftCard;
        }
    }
}
