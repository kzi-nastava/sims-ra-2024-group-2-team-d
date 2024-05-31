using BookingApp.Domain.Model;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.Guide
{
    public class ComplexTourReqViewModel : INotifyPropertyChanged
    {
        private MainService MainService { get; set; }
        public User LoggedInUser { get; set; }
        public ObservableCollection<ComplexTourRequest> ComplexTourRequests { get; set; }
        public ObservableCollection<TourRequest> AllTourRequests { get; set; }
        private TourRequest selectedTourRequest;
        public TourRequest SelectedTourRequest
        {
            get { return selectedTourRequest; }
            set
            {
                selectedTourRequest = value;
                OnPropertyChanged("SelectedTourRequest");
            }
        }
        public MyCommand AcceptCommand { get; set; }

        public ComplexTourReqViewModel(User user)
        {
            MainService = MainService.GetInstance();
            LoggedInUser = user;
            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>(MainService.ComplexTourRequestService.GetAll());
            LinkReqWithComplReq();
            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>(MainService.ComplexTourRequestService.GetAllForGuide(user));
            AllTourRequests = new ObservableCollection<TourRequest>();
            GetAllTourRequests();
            //AcceptCommand = new MyCommand();

        }

        public void LinkReqWithComplReq()
        {
            foreach(var request in ComplexTourRequests)
            {
                foreach(var id in request.TourRequestIds)
                {
                    request.TourRequests.Add(MainService.TourRequestService.GetById(id));
                }
            }
        }

        private void GetAllTourRequests()
        {
            foreach (var complexRequest in ComplexTourRequests)
            {
                foreach (var request in complexRequest.TourRequests)
                {
                    if(request.CurrentStatus == Status.OnHold)
                    {
                        AllTourRequests.Add(request);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
