using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.Guest1;
using BookingApp.View.Owner;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if(user.Password == txtPassword.Password)
                {
                    if(user.Role == Roles.OWNER)
                    {
                        OpenOwnerApplication(user);
                    }
                    else if(user.Role == Roles.GUEST)
                    {
                        OpenGuestApplication(user);
                    }
                    else if(user.Role == Roles.TOURIST)
                    {
                        OpenTouristApplication(user);
                    }
                    else if(user.Role == Roles.GUIDE)
                    {
                        OpenGuideApplication(user);
                    }
                }  
            }
            else
            {
                MessageBox.Show("Username or password is wrong!");
            }          
        }

        public void OpenGuideApplication(User user)
        {
            WelcomeGuide welcomeGuideWindow = new WelcomeGuide(user);
            welcomeGuideWindow.Show();
            Close();
        }
        public void OpenTouristApplication(User user)
        {
            TouristWindow touristWindow = new TouristWindow(user);
            touristWindow.Show();
            Close();
        }

        public void OpenGuestApplication(User user)
        {
            Guest1Window guest1Window = new Guest1Window(user);
            guest1Window.Show();
            Close();
        }

        public void OpenOwnerApplication(User user)
        {
            HomeWindow homeWindow = new HomeWindow(user);
            homeWindow.Show();
            Close();
        }
    }
}