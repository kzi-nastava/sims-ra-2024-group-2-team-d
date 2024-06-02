using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Services.IServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class ShowAllTouristsOnStandardTourRequestViewModel: IRequestClose, INotifyPropertyChanged
    {
        private ITouristService _touristService;
        public ObservableCollection<Tourist> Tourists { get; set; }
        public ICommand CloseCommand {  get; set; }
        private int _numberOfTourists { get; set; }

        public int NumberOfTourists
        {
            get { return _numberOfTourists; }
            set
            {
                if (_numberOfTourists != value)
                {
                    _numberOfTourists = value;
                    OnPropertyChanged(nameof(NumberOfTourists));
                }
            }
        }
        public event EventHandler<DialogCloseRequestedEventArgs> RequestClose;

        public ShowAllTouristsOnStandardTourRequestViewModel(TourRequestDTO tourRequest)
        {
            _touristService = Injector.Injector.CreateInstance<ITouristService>();
            Tourists = new ObservableCollection<Tourist>(_touristService.GetByIds(tourRequest.TouristsId));
            NumberOfTourists = tourRequest.NumberOfTourists;
            CloseCommand = new RelayCommand(Close);
        }

        event EventHandler<DialogCloseRequestedEventArgs> IRequestClose.RequestClose
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public void Close()
        {
            RequestClose?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
