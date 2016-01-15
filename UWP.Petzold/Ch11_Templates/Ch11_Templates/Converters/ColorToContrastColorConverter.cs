using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace Ch11_Templates.Converters {
    public class ColorToContrastColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            Color clr = (Color)value;
            double grayScale = 0.30 * clr.R + 0.59 * clr.G + 0.11 * clr.B;
            return grayScale > 128 ? Colors.Black : Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return value;
        }
    }
}
