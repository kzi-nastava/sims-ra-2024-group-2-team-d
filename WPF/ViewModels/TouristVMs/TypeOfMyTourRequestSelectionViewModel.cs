using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookingApp.Domain.Model;
using BookingApp.WPF.Views;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class TypeOfMyTourRequestSelectionViewModel
    {
        public User LoggedInUser { get; set; }

        public ICommand ShowMyStandardTourRequestsCommand { get; set; }
        public TypeOfMyTourRequestSelectionViewModel(User loggedInUser)
        {
            LoggedInUser = loggedInUser;
            ShowMyStandardTourRequestsCommand = new RelayCommand(ShowMyStandardTourRequests);
        }

        public void ShowMyStandardTourRequests()
        {
            MyStandardTourRequestsView view = new MyStandardTourRequestsView(LoggedInUser);
            view.Show();
        }

    }
}
