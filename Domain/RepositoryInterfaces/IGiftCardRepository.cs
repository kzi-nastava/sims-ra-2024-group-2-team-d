﻿using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IGiftCardRepository
    {
        public GiftCard Save(GiftCard giftCard);

        public List<GiftCard> GetAll();

        public GiftCard GetById(int id);

        public GiftCard? UpdateValidStatus(GiftCard newGiftCard);
    }
}
