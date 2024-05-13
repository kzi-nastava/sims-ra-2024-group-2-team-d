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
using BookingApp.Domain.Model;
using BookingApp.ViewModel;

namespace BookingApp.View.Guest1
{
    /// <summary>
    /// Interaction logic for GuestRatingsForm.xaml
    /// </summary>
    public partial class GuestRatingsForm : Window
    {
        public GuestRatingsForm(User user)
        {
            InitializeComponent();
            DataContext = new GuestRatingsViewModel(user);

        }
    }
}
