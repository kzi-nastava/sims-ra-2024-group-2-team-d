using BookingApp.Dto;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Windows.Input;
using System.ComponentModel;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class ShowAllTouristsOnStandardTourRequestViewModel: IRequestClose, INotifyPropertyChanged
    {
        private TouristService _touristService;
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
            _touristService = new TouristService(Injector.Injector.CreateInstance<ITouristRepository>());
            Tourists = new ObservableCollection<Tourist>(_touristService.GetByIds(tourRequest.TouristsId));
            NumberOfTourists = tourRequest.NumberOfTourists;
            CloseCommand = new RelayCommand(Close);
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
