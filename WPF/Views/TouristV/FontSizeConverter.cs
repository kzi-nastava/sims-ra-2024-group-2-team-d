using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BookingApp.WPF.Views.TouristV
{
    public class FontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double percentage && parameter is string originalSizeStr && double.TryParse(originalSizeStr, out double originalSize))
            {
                return originalSize * (percentage / 100);
            }
            return 12; // Default font size if conversion fails
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
