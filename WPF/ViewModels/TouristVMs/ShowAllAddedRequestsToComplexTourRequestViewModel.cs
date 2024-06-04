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
        public ICommand DeleteCommand {  get; set; }
        public ICommand EditCommand {  get; set; }
        public IDialogService DialogService { get; set; }
        public string DeleteOrEdit {  get; set; }

        public TourRequestDTO SelectedRequest {  get; set; }
        public ShowAllAddedRequestsToComplexTourRequestViewModel(ObservableCollection<TourRequestDTO> addedTourRequests, IDialogService dialogService)
        {
            AddedTourRequests = addedTourRequests;
            CloseCommand = new RelayCommand(Close);
            DeleteCommand = new RelayCommand(tourRequest => Delete((TourRequestDTO)tourRequest));
            DialogService = dialogService;
            SelectedRequest = new TourRequestDTO();
            EditCommand = new RelayCommand(tourRequest => Edit((TourRequestDTO)tourRequest));

        }

        public void Edit(TourRequestDTO tourRequest)
        {
            var confirmationViewModel = new ConfirmationDialogViewModel("Are you sure you want to edit this request?");
            bool? result = DialogService.ShowDialog(confirmationViewModel);
            if(result == true)
            {
                AddedTourRequests.Remove(tourRequest);
                DeleteOrEdit = "Edit";
                SelectedRequest = tourRequest;
                RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(true));
            }
        }

        public void Delete(TourRequestDTO tourRequest)
        {
            var confirmationViewModel = new ConfirmationDialogViewModel("Are you sure you want to delete this request?");
            bool? result = DialogService.ShowDialog(confirmationViewModel);
            if(result == true)
            {
                AddedTourRequests.Remove(tourRequest);
                DeleteOrEdit = "Delete";
                SelectedRequest = tourRequest;
            }
        }

        public void Close()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }
    }
}
