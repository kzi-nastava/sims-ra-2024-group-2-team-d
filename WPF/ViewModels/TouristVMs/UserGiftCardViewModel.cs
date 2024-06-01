using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class UserGiftCardViewModel: IRequestClose
    {
        public ObservableCollection<GiftCard> GiftCards { get; set; }
        public event EventHandler<DialogCloseRequestedEventArgs> RequestClose;
        public ICommand GoBackCommand {  get; set; }

        public UserGiftCardViewModel(ObservableCollection<GiftCard> giftCards)
        {
            GiftCards = giftCards;
            GoBackCommand = new RelayCommand(GoBack);
        }
        public void GoBack()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(false));
        }
    }
}
