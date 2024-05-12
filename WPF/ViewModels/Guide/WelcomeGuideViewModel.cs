using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.Guide
{
    public class WelcomeGuideViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public User LoggedInUser { get; set; }
        private string usernameText;
        public string UsernameText
        {
            get { return usernameText; }
            set
            {
                usernameText = value;
                OnPropertyChanged("UsernameText");
            }
        }
        public MyCommand LetsStartCommand { get; set; }
        public MyCommand CloseCommand { get; set; }

        public WelcomeGuideViewModel(User user, Action closeAction)
        {
            MainService = MainService.GetInstance();
            LoggedInUser = user;
            UsernameText = LoggedInUser.Username;
            LetsStartCommand = new MyCommand(() =>
            {
                Start();
                closeAction();
            });
            CloseCommand = new MyCommand(() =>
            {              
                closeAction();
            });
        }

        private void Start()
        {
            GuideWindow guideWindow = new GuideWindow(LoggedInUser);
            guideWindow.Show();
            //Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
