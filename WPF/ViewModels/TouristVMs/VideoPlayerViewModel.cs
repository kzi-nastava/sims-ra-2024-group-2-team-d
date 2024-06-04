using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class VideoPlayerViewModel : INotifyPropertyChanged
    {
        private string videoPath;
        private bool isDraggingSlider;

        public string VideoPath
        {
            get => videoPath;
            set
            {
                videoPath = value;
                OnPropertyChanged(nameof(VideoPath));
            }
        }

        private double currentPosition;
        private double videoDuration;

        public double CurrentPosition
        {
            get => currentPosition;
            set
            {
                currentPosition = value;
                OnPropertyChanged(nameof(CurrentPosition));
            }
        }

        public double VideoDuration
        {
            get => videoDuration;
            set
            {
                videoDuration = value;
                OnPropertyChanged(nameof(VideoDuration));
            }
        }

        public bool IsDraggingSlider
        {
            get => isDraggingSlider;
            set
            {
                isDraggingSlider = value;
                OnPropertyChanged(nameof(IsDraggingSlider));
            }
        }


        public ICommand PlayCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand RewindCommand { get; }
        public ICommand FastForwardCommand { get; }
        public ICommand StartDragCommand { get; }
        public ICommand EndDragCommand { get; }
        public ICommand BackButtonCommand { get; set; }
        private MainViewModel _mainViewModel;
        public User LoggedInUser {  get; set; }
        public IDialogService _dialogService;


        public VideoPlayerViewModel(MainViewModel mainViewModel, User user, IDialogService dialogService)
        {
            // Primer video putanje, možete ovo dinamički postaviti
            VideoPath = "../../../Resources/TutorialVideo/TouristAppTutorial.mkv";

            PlayCommand = new RelayCommand(Play);
            PauseCommand = new RelayCommand(Pause);
            StopCommand = new RelayCommand(Stop);
            RewindCommand = new RelayCommand(Rewind);
            FastForwardCommand = new RelayCommand(FastForward);
            StartDragCommand = new RelayCommand(StartDrag);
            EndDragCommand = new RelayCommand(EndDrag);
            BackButtonCommand = new RelayCommand(GoBack);
            _mainViewModel = mainViewModel;
            _dialogService = dialogService;
            LoggedInUser = user;
        }

        public void GoBack()
        {
            _mainViewModel.SwitchView(new TouristHomeViewModel(_mainViewModel, LoggedInUser, _dialogService));

        }

        private void StartDrag()
        {
            IsDraggingSlider = true;
        }

        private void EndDrag()
        {
            IsDraggingSlider = false;
            OnRequestSetPosition(CurrentPosition);
        }

        private void Play()
        {
            OnRequestAction("Play");
        }

        private void Pause()
        {
            OnRequestAction("Pause");
        }

        private void Stop()
        {
            OnRequestAction("Stop");
        }

        private void Rewind()
        {
            OnRequestAction("Rewind");
        }

        private void FastForward()
        {
            OnRequestAction("FastForward");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event Action<string> RequestAction;
        protected virtual void OnRequestAction(string action)
        {
            RequestAction?.Invoke(action);
        }

        public event Action<double> RequestSetPosition;
        protected virtual void OnRequestSetPosition(double position)
        {
            RequestSetPosition?.Invoke(position);
        }
    }
}
