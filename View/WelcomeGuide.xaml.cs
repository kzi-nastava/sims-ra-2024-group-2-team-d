using BookingApp.Model;
using BookingApp.ViewModel.Guide;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for WelcomeGuide.xaml
    /// </summary>
    public partial class WelcomeGuide : Window
    {
        public WelcomeGuide(User user)
        {
            InitializeComponent();
            DataContext = new WelcomeGuideViewModel(user);
        }
    }
}
