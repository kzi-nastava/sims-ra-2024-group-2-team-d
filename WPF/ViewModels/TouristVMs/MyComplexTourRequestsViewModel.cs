using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Services;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.ObjectModel;
using BookingApp.Dto;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class MyComplexTourRequestsViewModel
    {
        public User LoggedInUser {  get; set; }
        public MainViewModel MainViewModel { get; set; }
        public ComplexTourRequestService ComplexTourRequestService { get; set; }
        public ObservableCollection<ComplexTourRequestDTO> MyComplexTourRequests {  get; set; }
        public ICommand InfoCommand {  get; set; }
        public ICommand GoBackCommand {  get; set; }
        public readonly IDialogService _dialogService;
        public MyComplexTourRequestsViewModel(MainViewModel mainViewModel, User loggedInUser, IDialogService dialogService) { 
            MainViewModel = mainViewModel;
            LoggedInUser = loggedInUser;
            ComplexTourRequestService = new ComplexTourRequestService(Injector.Injector.CreateInstance<IComplexTourRequestRepository>(), Injector.Injector.CreateInstance<ITourRequestRepository>());
            ComplexTourRequestService.ChangeStatusOfComplexTourRequest();
            MyComplexTourRequests = new ObservableCollection<ComplexTourRequestDTO>();
            InfoCommand = new RelayCommand(complexTourRequestDTO => ShowAllPartsOfComplexTourRequest((ComplexTourRequestDTO)complexTourRequestDTO));
            ToDto();
            GoBackCommand = new RelayCommand(GoBack);
            _dialogService = dialogService;
        }


        public void GoBack()
        {
            MainViewModel.SwitchView(new TouristHomeViewModel(MainViewModel, LoggedInUser, new DialogService()));
        }

        public void ShowAllPartsOfComplexTourRequest(ComplexTourRequestDTO complexTourRequestDTO)
        {
            MainViewModel.SwitchView(new AllRequestsInComplexTourRequestViewModel(MainViewModel, complexTourRequestDTO.TourRequests, LoggedInUser, _dialogService));
        }
        

        public void ToDto()
        {
            List<ComplexTourRequest> complexTourRequests = ComplexTourRequestService.GetByUserId(LoggedInUser.Id);
            foreach(var complexTourRequest in complexTourRequests)
            {
                ComplexTourRequestDTO complexTourRequestDTO = new ComplexTourRequestDTO(complexTourRequest);
                MyComplexTourRequests.Add(complexTourRequestDTO);
            }
        }
    }
}
