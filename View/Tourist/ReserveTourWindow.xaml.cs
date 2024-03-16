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

namespace BookingApp.View.Tourist
{
    /// <summary>
    /// Interaction logic for ReserveTourWindow.xaml
    /// </summary>
    public partial class ReserveTourWindow : Window
    {
        public ReserveTourWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void AddTouristButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
