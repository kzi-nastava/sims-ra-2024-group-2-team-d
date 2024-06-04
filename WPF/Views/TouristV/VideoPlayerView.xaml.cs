using BookingApp.WPF.ViewModels.TouristVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.TouristV
{
    /// <summary>
    /// Interaction logic for VideoPlayerView.xaml
    /// </summary>
    public partial class VideoPlayerView : UserControl
    {
        public VideoPlayerView()
        {
            InitializeComponent();
            this.Loaded += VideoPlayerView_Loaded;
            DataContextChanged += VideoPlayerView_DataContextChanged;
        }

        private void VideoPlayerView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is VideoPlayerViewModel viewModel)
            {
                if (mediaElement.Source != null)
                {
                    mediaElement.Play();
                }

                CompositionTarget.Rendering += (s, args) =>
                {
                    if (!viewModel.IsDraggingSlider)
                    {
                        viewModel.CurrentPosition = mediaElement.Position.TotalSeconds;
                    }
                };
            }
        }

        private void VideoPlayerView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is VideoPlayerViewModel oldViewModel)
            {
                oldViewModel.RequestAction -= OnRequestAction;
                oldViewModel.RequestSetPosition -= OnRequestSetPosition;
            }

            if (e.NewValue is VideoPlayerViewModel newViewModel)
            {
                newViewModel.RequestAction += OnRequestAction;
                newViewModel.RequestSetPosition += OnRequestSetPosition;
            }
        }

        private void OnRequestAction(string action)
        {
            switch (action)
            {
                case "Play":
                    mediaElement.Play();
                    break;
                case "Pause":
                    mediaElement.Pause();
                    break;
                case "Stop":
                    mediaElement.Stop();
                    break;
            }
        }

        private void OnRequestSetPosition(double position)
        {
            mediaElement.Position = TimeSpan.FromSeconds(position);
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (DataContext is VideoPlayerViewModel viewModel)
            {
                viewModel.VideoDuration = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            }
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (DataContext is VideoPlayerViewModel viewModel)
            {
                viewModel.CurrentPosition = 0;
            }
        }

        private void MediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            // Handle media load failure
        }

        private void Slider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is VideoPlayerViewModel viewModel)
            {
                viewModel.StartDragCommand.Execute(null);
            }
        }

        private void Slider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is VideoPlayerViewModel viewModel)
            {
                viewModel.EndDragCommand.Execute(null);
            }
        }
    }
}
