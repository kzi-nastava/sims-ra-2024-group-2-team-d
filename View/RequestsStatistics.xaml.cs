using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.ViewModel.Guide;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for RequestsStatistics.xaml
    /// </summary>
    public partial class RequestsStatistics : Window
    {
        //public User LoggedInUser { get; set; }
        //public string Location { get; set; }
        //public string Lang { get; set; }
        //public TourRequestRepository _tourRequestRepository { get; set; }

        public RequestsStatistics(User user)
        {
            InitializeComponent();
            DataContext = new RequestsStatisticsViewModel(user);
            //LoggedInUser = user;
            //DataContext = this;
            //_tourRequestRepository = new TourRequestRepository();          
            //Location = _tourRequestRepository.FindMostWantedLocInLastYear();
            //Lang = _tourRequestRepository.FindMostWantedLangInLastYear();        
        }

        //private void CrLoc_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void CrLang_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
