using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class UserGiftCardViewModel
    {
        public ObservableCollection<GiftCard> GiftCards { get; set; }

        public UserGiftCardViewModel(ObservableCollection<GiftCard> giftCards)
        {
            GiftCards = giftCards;
        }
    }
}
