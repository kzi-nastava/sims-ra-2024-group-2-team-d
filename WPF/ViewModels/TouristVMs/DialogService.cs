using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;
using BookingApp.WPF.Views.TouristV;
using BookingApp.WPF.Views;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public interface IDialogService
    {
        bool? ShowDialog(object viewModel);
    }

    public class DialogService : IDialogService
    {
        public bool? ShowDialog(object viewModel)
        {
            var currentMainWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            var window = new Window
            {
                Content = CreateContent(viewModel),
                Owner = currentMainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStyle = WindowStyle.ToolWindow,
                ShowInTaskbar = false,
                Opacity = 0
            };

            var fadeInAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5)));
            var fadeOutAnimation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.2)));

            var openStoryboard = new Storyboard();
            openStoryboard.Children.Add(fadeInAnimation);

            Storyboard.SetTarget(fadeInAnimation, window);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity"));

            var closeStoryboard = new Storyboard();
            closeStoryboard.Children.Add(fadeOutAnimation);

            Storyboard.SetTarget(fadeOutAnimation, window);
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath("Opacity"));

            window.Loaded += (sender, args) => openStoryboard.Begin(window);

            if (viewModel is IRequestClose requestClose)
            {
                requestClose.RequestClose += (s, e) =>
                {
                    closeStoryboard.Completed += (s2, e2) =>
                    {
                        window.DialogResult = e.DialogResult;
                        window.Close();
                    };
                    closeStoryboard.Begin(window);
                };
            }

            return window.ShowDialog();
        }

        private FrameworkElement CreateContent(object viewModel)
        {
            if (viewModel is NumberOfTouristInsertionViewModel)
            {
                return new NumberOfTouristInsertionView { DataContext = viewModel };
            }
            if (viewModel is TypeOfTourRequestSelectionViewModel)
            {
                return new TypeOfTourRequestSelectionView { DataContext = viewModel };
            }
            if (viewModel is TypeOfMyTourRequestSelectionViewModel)
            {
                return new TypeOfMyTourRequestSelectionView { DataContext = viewModel };
            }
            // Dodajte ostale view modele i odgovarajuće view-ove ovde
            return null;
        }
    }

    public interface IRequestClose
    {
        event EventHandler<DialogCloseRequestedEventArgs> RequestClose;
    }

    public class DialogCloseRequestedEventArgs : EventArgs
    {
        public bool DialogResult { get; }
        public string SelectedOption { get; }
        public DialogCloseRequestedEventArgs(bool dialogResult, string selectedOption)
        {
            DialogResult = dialogResult;
            SelectedOption = selectedOption;
        }
        public DialogCloseRequestedEventArgs(bool dialogResult)
        {
            DialogResult = dialogResult;        
        }
    }
}
