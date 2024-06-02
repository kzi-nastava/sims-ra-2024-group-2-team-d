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
using BookingApp.Dto;
using BookingApp.WPF.ViewModels.TouristVMs;

namespace BookingApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for ShowAllTouristsOnStandardTourRequestView.xaml
    /// </summary>
    public partial class ShowAllTouristsOnStandardTourRequestView : UserControl
    {
        public ShowAllTouristsOnStandardTourRequestView()
        {
            InitializeComponent();
            //DataContext = new ShowAllTouristsOnStandardTourRequestViewModel(tourRequest);
        }
    }
}
