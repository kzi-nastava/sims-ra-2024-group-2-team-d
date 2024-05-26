using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

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
            // Pronađi trenutni glavni prozor
            var currentMainWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            var window = new Window
            {
                Content = new ContentControl
                {
                    Content = viewModel,
                    DataContext = viewModel
                },
                Owner = currentMainWindow,  // Postavi vlasnika prozora na trenutno aktivni prozor
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStyle = WindowStyle.ToolWindow,
                ShowInTaskbar = false
            };

            if (viewModel is IRequestClose requestClose)
            {
                requestClose.RequestClose += (s, e) =>
                {
                    window.DialogResult = e.DialogResult;
                    window.Close();
                };
            }

            return window.ShowDialog();
        }
    }

    public interface IRequestClose
    {
        event EventHandler<DialogCloseRequestedEventArgs> RequestClose;
    }

    public class DialogCloseRequestedEventArgs : EventArgs
    {
        public bool DialogResult { get; }

        public DialogCloseRequestedEventArgs(bool dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}
