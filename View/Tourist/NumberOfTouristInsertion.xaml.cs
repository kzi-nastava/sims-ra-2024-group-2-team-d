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
    /// Interaction logic for NumberOfTouristInsertion.xaml
    /// </summary>
    public partial class NumberOfTouristInsertion : Window
    {
        public int EmptySpots {  get; set; }

        public int MaxTourists {  get; set; }
        public NumberOfTouristInsertion(int emptySpots, int maxTourists)
        {
            InitializeComponent();
            DataContext = this;
            EmptySpots = emptySpots;
            MaxTourists = maxTourists;

        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            int enteredNumber = int.Parse(enteredNumberTextBox.Text);
            if (EmptySpots >= enteredNumber)
            {
                ReserveTourWindow reserveTourWindow = new ReserveTourWindow();
                reserveTourWindow.Show();
                this.Close();
            }
            else
            {

            }
        }
    }
}
