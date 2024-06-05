using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace BookingApp.WPF.Views.TouristV
{
    public class TextBoxInputBehavior : Behavior<TextBox>
    {
        private static readonly Regex _regex = new Regex("[^a-zA-Z-,]+"); // Regex for allowed characters

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            DataObject.AddPastingHandler(AssociatedObject, OnPaste);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            DataObject.RemovePastingHandler(AssociatedObject, OnPaste);
        }

        private void OnPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}
