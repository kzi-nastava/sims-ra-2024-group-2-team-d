using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookingApp.WPF.Views;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class TypeOfTourRequestSelectionViewModel
    {
        public User LoggedInUser { get; set; }
        public ICommand StandardTourRequestCreationCommand { get; set; }
        public TypeOfTourRequestSelectionViewModel(User loggedInUser)
        {
            StandardTourRequestCreationCommand = new RelayCommand(OpenStandardTourCreationWindow);
            LoggedInUser = loggedInUser;
        }

        public void OpenStandardTourCreationWindow()
        {
            CreateTourRequestView createTourRequestView = new CreateTourRequestView(LoggedInUser);
            createTourRequestView.Show();
        }
    }
}
