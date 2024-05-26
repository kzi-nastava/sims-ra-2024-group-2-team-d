using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.View.TouristApp;
using BookingApp.WPF.ViewModels.TouristVMs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.TouristV
{
    /// <summary>
    /// Interaction logic for TouristHomeView.xaml
    /// </summary>
    public partial class TouristHomeView : UserControl
    {
       public TouristHomeView(MainViewModel mainViewModel, User loggedInUser, IDialogService dialogService) {
            InitializeComponent();
            this.DataContext = new TouristHomeViewModel(mainViewModel, loggedInUser, dialogService);
        }

        public TouristHomeView()
        {
            InitializeComponent();
        }
    }
}
