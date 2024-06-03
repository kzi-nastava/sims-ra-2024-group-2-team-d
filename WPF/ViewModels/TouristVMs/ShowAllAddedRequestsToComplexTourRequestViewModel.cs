using BookingApp.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class ShowAllAddedRequestsToComplexTourRequestViewModel: IRequestClose
    {
        public ObservableCollection<TourRequestDTO> AddedTourRequests {  get; set; }
        public event EventHandler<DialogCloseRequestedEventArgs> RequestClose;

        public ICommand CloseCommand { get; set; }
        public ShowAllAddedRequestsToComplexTourRequestViewModel(ObservableCollection<TourRequestDTO> addedTourRequests)
        {
            AddedTourRequests = addedTourRequests;
            CloseCommand = new RelayCommand(Close);
        }

        public void Close()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }
    }
}
