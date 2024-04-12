using BookingApp.Model;
using BookingApp.Repository;
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
using System.Xml.Linq;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AllTours.xaml
    /// </summary>
    public partial class AllTours : Window
    {
        public User LoggedInUser { get; set; }
        public TourInstance TourInstance { get; set; }
        private readonly TourInstanceRepository _instanceRepository;
        public static ObservableCollection<TourInstance> TourInstances { get; set; }

        public TourInstance SelectedTourInstance { get; set; }


        public AllTours(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _instanceRepository = new TourInstanceRepository();
            TourInstances = new ObservableCollection<TourInstance>(_instanceRepository.GetByUser(user));
        }



        private void Delete(object sender, RoutedEventArgs e)
        {
            if (SelectedTourInstance != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete comment",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _instanceRepository.Delete(SelectedTourInstance);
                    TourInstances.Remove(SelectedTourInstance);
                }
            }
        }


    }
}
