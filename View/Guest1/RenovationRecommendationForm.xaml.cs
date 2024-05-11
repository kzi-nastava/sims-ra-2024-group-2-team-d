using System;
using System.Collections.Generic;
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
using BookingApp.Model;
using BookingApp.ViewModel;

namespace BookingApp.View.Guest1
{
    /// <summary>
    /// Interaction logic for RenovationRecommendationForm.xaml
    /// </summary>
    public partial class RenovationRecommendationForm : Window
    {
        public RenovationRecommendationForm(Reservation reservation)
        {
            InitializeComponent();
            DataContext = new RenovationRecommendationViewModel(reservation);
        }

        private void checkBoxLevel1_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}