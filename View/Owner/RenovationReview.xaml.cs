using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.ViewModel.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for RenovationReview.xaml
    /// </summary>
    public partial class RenovationReview : Window, INotifyPropertyChanged
    {
        private readonly AccommodationRepository _accommodationRepository;
        private readonly RenovationRepository _renovationRepository;
        public MyCommand onAccept { get; set; }

        private ObservableCollection<Renovation> renovations;

        public ObservableCollection<Renovation> Renovations { get { return renovations; }
            set
            {
                renovations = value;
                OnPropertyChanged("Renovations");
            }
        }

        private readonly User _user;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Renovation SelectedRenovation { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public RenovationReview(User user)
        {
            InitializeComponent();
            onAccept = new MyCommand(Accept);
            _accommodationRepository = new AccommodationRepository();
            _renovationRepository = new RenovationRepository();
            Renovation r = new Renovation();
            _user = user;
            List<Renovation> help = new List<Renovation>( (_renovationRepository.GetAllByUserId(_user.Id)));//da radi preko id
            Accommodations = new ObservableCollection<Accommodation>();
            SelectedRenovation = new Renovation();
            //DataContext = Renovations;
            // Poveži svaku renovaciju sa odgovarajućim smještajem
            
            foreach (Renovation renovation in help)
            {//
                renovation.Accomodation= _accommodationRepository.GetById(renovation.AccomodationId);
              //  renovation._startDay = renovation.RenovationDateRange.StartDate;
              //  renovation._endDay = renovation.RenovationDateRange.EndDate;
                //help.Add(renovation);

            }
            Renovations = new ObservableCollection<Renovation>(help);
            DataContext = this;
           

        }
        public void Accept()
        {
            if(!(SelectedRenovation._startDay>=DateTime.Now.AddDays(-5)))
            {
                MessageBox.Show("Ne mozete otkazati renoviranje");
            }
            else
            {
                
                _renovationRepository.Delete(SelectedRenovation);
                Renovations.Remove(SelectedRenovation);
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

