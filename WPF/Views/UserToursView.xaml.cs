using BookingApp.Model;
using BookingApp.WPF.ViewModels;
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

namespace BookingApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for UserToursView.xaml
    /// </summary>
    public partial class UserToursView : Window
    {
        public UserToursView(ObservableCollection<TourInstance> userTours)
        {
            InitializeComponent();
            this.DataContext = new UserToursViewModel(userTours);
        }
    }
}
