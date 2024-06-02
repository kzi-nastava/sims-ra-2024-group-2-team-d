using BookingApp.Domain.Model;
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
    public class AllRequestsInComplexTourRequestViewModel
    {
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; }
        public User LoggedInUser {  get; set; }

        public ICommand GoBackCommand {  get; set; }
        public ICommand ShowAllTouristsCommand {  get; set; }

        public MainViewModel MainViewModel { get; set; }

        private readonly IDialogService _dialogSerivce;
        public AllRequestsInComplexTourRequestViewModel(MainViewModel mainViewModel, List<TourRequest> tourRequests, User loggedInUser, IDialogService dialogService) { 
            TourRequests = new ObservableCollection<TourRequestDTO>(ConvertModelToDTO(tourRequests));
            LoggedInUser = loggedInUser;
            MainViewModel = mainViewModel;
            GoBackCommand = new RelayCommand(GoBack);
            ShowAllTouristsCommand = new RelayCommand(dto=> ShowAllTourists((TourRequestDTO)dto));
            _dialogSerivce = dialogService;
        }

        public void ShowAllTourists(TourRequestDTO dto)
        {
            var viewModel = new ShowAllTouristsOnStandardTourRequestViewModel(dto);
            bool? result = _dialogSerivce.ShowDialog(viewModel);
        }

        public void GoBack()
        {
            MainViewModel.SwitchView(new MyComplexTourRequestsViewModel(MainViewModel, LoggedInUser, _dialogSerivce));
        }

        public List<TourRequestDTO> ConvertModelToDTO(List<TourRequest> tourRequests)
        {
            List<TourRequestDTO> dtos = new List<TourRequestDTO>();
            foreach (var tourRequest in tourRequests)
            {
                dtos.Add(new TourRequestDTO(tourRequest));
            }
            return dtos;
        }
    }
}
