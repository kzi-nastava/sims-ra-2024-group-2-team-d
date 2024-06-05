using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Views.TouristV
{
    public class FontSizeManager: INotifyPropertyChanged
    {
        private static readonly FontSizeManager _instance = new FontSizeManager();
        public static FontSizeManager Instance => _instance;

        private double _fontSizePercentage;

        public double FontSizePercentage
        {
            get => _fontSizePercentage;
            set
            {
                if (_fontSizePercentage != value)
                {
                    _fontSizePercentage = value;
                    OnPropertyChanged(nameof(FontSizePercentage));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private FontSizeManager()
        {
            _fontSizePercentage = 100; // Default value, 100%
        }
    }
}
