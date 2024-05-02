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
using BookingApp.WPF.ViewModels;

namespace BookingApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for ShowMorePicturesView.xaml
    /// </summary>
    public partial class ShowMorePicturesView : Window
    {
        public ShowMorePicturesView(TourInstance tourInstance)
        {
            InitializeComponent();
            DataContext = new ShowMorePicturesViewModel(tourInstance);
        }
    }
}
