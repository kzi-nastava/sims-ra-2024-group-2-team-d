using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Linq;
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
        private readonly ReservationRepository _reservationRepository;
        private readonly User _user;
        
        public AccommodationDto Accommodation { get; set; }
        public string ImagePath {  get; set; }
       

        public RegisterAccomodation(User user)
        {
            InitializeComponent();
            DataContext = this;
            _user = user;
            _accommodationRepository = new AccommodationRepository();
            _reservationRepository = new ReservationRepository();

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
                MessageBox.Show("Uspesno ste registrovali novi smjestaj.");
            } else
            {
                ErrorMessage.Visibility = Visibility.Visible;
                ErrorMessage.Content = isValidAccomodation;
            }

        }

        private void AddImageFromPC_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
            {
                ImageTextbox.Text = openFileDialog.FileName;
            }
        }


        private void CancelButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            if (Accommodation.Images is null)
            {
                Accommodation.Images = new System.Collections.Generic.List<string>();
            }
            Accommodation.Images.Add(ImagePath);
            ImagePath = String.Empty;
            OnPropertyChanged("ImagePath");
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
