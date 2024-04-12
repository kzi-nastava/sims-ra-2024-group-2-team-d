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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for FollowingTourLive.xaml
    /// </summary>
    public partial class FollowingTourLive : Window
    {
        public TourInstance TourInstance;

        public static ObservableCollection<KeyPoint> KeyPoints { get; set; }
        private readonly KeyPointRepository _keyPointRepository;


        public FollowingTourLive(TourInstance ti)
        {
            InitializeComponent();
            this.TourInstance = ti;
            _keyPointRepository = new KeyPointRepository();
            KeyPoints = new ObservableCollection<KeyPoint>(_keyPointRepository.GetByTourId(TourInstance.TourId));

        }
    }
}
