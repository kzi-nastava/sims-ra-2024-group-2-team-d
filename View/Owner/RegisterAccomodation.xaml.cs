using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for RegisterAccomodation.xaml
    /// </summary>
    public partial class RegisterAccomodation : Window, INotifyPropertyChanged
    {
        private readonly AccommodationRepository _accommodationRepository;
        
        public AccommodationDto Accommodation { get; set; }
       

        public RegisterAccomodation(User user)
        {
            InitializeComponent();
            DataContext = this;
            _accommodationRepository = new AccommodationRepository();

            Accommodation = new AccommodationDto();
            Accommodation.UserId = user.Id;
            

        }

        private void NewAccommodationRegistration(object sender, RoutedEventArgs e)
        {
            string isValidAccomodation = Accommodation.IsValid;

            if (isValidAccomodation == string.Empty)
            {
                Accommodation newAccommodation = Accommodation.ToModel();
                _accommodationRepository.Save(newAccommodation);
                this.Close();
            } else
            {
                ErrorMessage.Visibility = Visibility.Visible;
                ErrorMessage.Content = isValidAccomodation;
            }

        }




        private void CancelButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
