using BookingApp.Repository;
using BookingApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class DeclineRequestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly ChangeReservationRequestRepository _changeReservationRequestRepository;
        public int RequestId { get; set; }

        private string _ownerComment;
        private BaseService baseService { get; set; }
        public string OwnerComment
        {
            get { return _ownerComment; }
            set
            {
                _ownerComment = value;
                OnPropertyChanged("Description");
            }
        }

        public RelayCommand OKCommand { get; set; }

        public DeclineRequestViewModel(int requestId)
        {
            baseService = new BaseService();
            _changeReservationRequestRepository = new ChangeReservationRequestRepository();
            RequestId = requestId;
            OwnerComment = string.Empty;

            OKCommand = new RelayCommand(Button_Click);
        }

        private void Button_Click(object parameter)
        {
            _changeReservationRequestRepository.DeclineRequest(RequestId, OwnerComment);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
