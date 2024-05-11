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
using BookingApp.WPF.ViewModels;

namespace BookingApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for PlotGraphView.xaml
    /// </summary>
    public partial class PlotGraphView : Window
    {
        public PlotGraphView(Dictionary<string,int> keyValuePairs, string xAxisTitle)
        {
            InitializeComponent();
            DataContext = new PlotGraphViewModel(keyValuePairs, xAxisTitle);
        }
    }
}
