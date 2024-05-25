using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels.TouristVMs;

namespace BookingApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for UserGiftCardView.xaml
    /// </summary>
    public partial class UserGiftCardView : Window
    {
        public UserGiftCardView(ObservableCollection<GiftCard> giftCards)
        {
            InitializeComponent();
            this.DataContext = new UserGiftCardViewModel(giftCards);
        }
    }
}
